using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using Harita.Properties;

namespace Harita
{
  public partial class Form1 : Form
  {
    private Bilgilendirme bilgiForm;
    private List<ImagePoint> landmarks;
    private Bitmap markerImage;

    public Form1()
    {
      InitializeComponent();
    }


    private IDictionary<string, Image> ImageCache { get; set; }

    private bool IsUpdatingMap { get; set; }

    private int Layer { get; set; }

    private List<MapLayerData> LayerData { get; set; }

    private bool ResetZoomOnUpdate { get; set; }

    private int VirtualZoom { get; set; }

    private int FindNearestLayer(int zoom)
    {
      int result;

      result = -1;

      for (var i = 0; i < LayerData.Count; i++)
      {
        MapLayerData data;

        data = LayerData[i];

        if (zoom >= data.LowerZoom && zoom < data.UpperZoom)
        {
          result = i;
          break;
        }
      }

      return result;
    }

    private void UpdateMap()
    {
      if (!IsUpdatingMap)
      {
        int mapLayer;

        IsUpdatingMap = true;

        mapLayer = FindNearestLayer(VirtualZoom);

        if (mapLayer != -1 && mapLayer != Layer)
        {
          MapLayerData data;
          Image newImage;
          RectangleF currentViewport;
          RectangleF newViewport;
          Size currentSize;
          float vAspectRatio;
          float hAspectRatio;

          Layer = mapLayer;
          data = LayerData[mapLayer];
          //this.mapNameToolStripStatusLabel.Text = data.Name;

          newImage = GetMapImage(data.Name);
          currentViewport = imageBox.GetSourceImageRegion();
          currentSize = imageBox.Image.Size;

          hAspectRatio = newImage.Width / (float) currentSize.Width;
          vAspectRatio = newImage.Height / (float) currentSize.Height;

          imageBox.BeginUpdate();
          imageBox.Image = newImage;
          newViewport = new RectangleF(
            currentViewport.X * hAspectRatio,
            currentViewport.Y * vAspectRatio,
            currentViewport.Width * hAspectRatio,
            currentViewport.Height * vAspectRatio);
          imageBox.ZoomToRegion(newViewport);

          if (ResetZoomOnUpdate)
          {
            ResetZoomOnUpdate = false;
            imageBox.Zoom = 100;
            imageBox.CenterToImage();
          }

          imageBox.EndUpdate();
        }

        IsUpdatingMap = false;
      }
    }

    private void AddLandmark(string name, Point point, string type)
    {
      Debug.Print("Added landmark: {0}", point);

      landmarks.Add(new ImagePoint(name, new PointF(point.X, point.Y), type));
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      markerImage = Resources.MapMarker;

      landmarks = new List<ImagePoint>();
      AddLandmark("Britain", new Point(182, 209), "Şehir");
      AddLandmark("Trinsic", new Point(234, 342), "Şehir");
      AddLandmark("Minoc", new Point(309, 62), "Şehir");


      //this.imagePoints = new List<ImagePoint>();
      //this.imagePoints.AddRange(
      //  new[]
      //    {
      //      new ImagePoint { color = "Red", label = "Test", location = new PointF(2360 / 8, 2840 / 8) },
      //      new ImagePoint { color = "Red", label = "Test", location = new PointF(400, 400) }
      //    });
      LayerData = new List<MapLayerData>();

      // add some map layers for the different zoom levels
      AddLayer("MAP25166902-8", int.MinValue, 2, 1);
      AddLayer("MAP25166902-4", 2, 4, 2);
      AddLayer("MAP25166902-2", 4, 8, 4);
      AddLayer("MAP25166902-1", 8, int.MaxValue, 8);

      // load the lowest detail map
      imageBox.Image = GetMapImage("MAP25166902-8");

      // now zoom in a bit
      VirtualZoom = 1;
      UpdateMap();
      imageBox.Zoom = 100;
      imageBox.CenterAt(234, 342);

      // imageBox.ZoomToFit();
    }

    private void AddLayer(string name, int lowerZoom, int upperZoom, int zoomLevel)
    {
      // The larger map sizes (>map50) are 80MB, so I'm not including them in the GitHub repository.
      // Therefore, just silently skip any missing maps without raising an error
      if (File.Exists(GetMapFileName(name)))
      {
        MapLayerData data;

        data = new MapLayerData();
        data.Name = name;
        data.UpperZoom = upperZoom;
        data.LowerZoom = lowerZoom;
        data.ZoomLevel = zoomLevel;

        LayerData.Add(data);
      }
    }

    private Image GetMapImage(string name)
    {
      Image result;

      if (ImageCache == null) ImageCache = new Dictionary<string, Image>();

      if (!ImageCache.TryGetValue(name, out result))
      {
        //this.SetStatus("Loading image...");

        result = Image.FromFile(GetMapFileName(name));

        ImageCache.Add(name, result);

        //this.SetStatus(string.Empty);
      }

      //this.SetMessage(string.Format("Switching to image {0}.jpg", name));

      return result;
    }

    private string GetMapFileName(string name)
    {
      return Path.GetFullPath(
        Path.Combine(
          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\maps"),
          Path.ChangeExtension(name, ".bmp")));
    }

    private void ImageBox_Paint(object sender, PaintEventArgs e)
    {
      Graphics g;
      GraphicsState originalState;
      Size scaledSize;
      Size originalSize;
      Size drawSize;
      bool scaleAdornmentSize;

      scaleAdornmentSize = false;

      g = e.Graphics;

      originalState = g.Save();

      // Work out the size of the marker graphic according to the current zoom level
      originalSize = markerImage.Size;
      scaledSize = imageBox.GetScaledSize(originalSize);
      drawSize = scaleAdornmentSize ? scaledSize : originalSize;


      foreach (var landmark in landmarks)
      {
        PointF location;

        var yeniLandmark = landmark;

        var findNearestLayer = FindNearestLayer(VirtualZoom);
        if (LayerData.Count <= findNearestLayer || findNearestLayer >= 0)
        {
          var div = LayerData[findNearestLayer].ZoomLevel;
          var yeniLandmarkLocation = yeniLandmark.location;
          yeniLandmarkLocation.X *= div;
          yeniLandmarkLocation.Y *= div;

          // Work out the location of the marker graphic according to the current zoom level and scroll offset
          location = imageBox.GetOffsetPoint(yeniLandmarkLocation);

          // adjust the location so that the image is displayed above the location and centered to it
          location.Y -= drawSize.Height;
          location.X -= drawSize.Width >> 1;

          // Draw the marker
          g.InterpolationMode = InterpolationMode.NearestNeighbor;
          g.DrawImage(
            markerImage,
            new Rectangle((int) location.X, (int) location.Y, drawSize.Width, drawSize.Height),
            new Rectangle(Point.Empty, originalSize),
            GraphicsUnit.Pixel);
        }
      }

      g.Restore(originalState);
    }

    private void ImageBox_Zoomed(object sender, ImageBoxZoomEventArgs e)
    {
      if ((e.Source & ImageBoxActionSources.User) == ImageBoxActionSources.User)
      {
        if ((e.Actions & ImageBoxZoomActions.ActualSize) == ImageBoxZoomActions.ActualSize)
        {
          VirtualZoom = 0;
          ResetZoomOnUpdate = true;
        }
        else if ((e.Actions & ImageBoxZoomActions.ZoomIn) == ImageBoxZoomActions.ZoomIn)
        {
          VirtualZoom++;
        }
        else if ((e.Actions & ImageBoxZoomActions.ZoomOut) == ImageBoxZoomActions.ZoomOut)
        {
          VirtualZoom--;
        }

        // TODO: Currently the ZoomChanged and Zoomed events are raised after the zoom level has changed, but before any
        // actions such as modifying scrollbars occur. This means methods such as GetSourceImageRegion will return the
        // wrong X and Y values. Until this is fixed, using a timer to trigger the change.
        // However, if you had lots of map changes to make then using a timer would be a good idea regardless; for example
        // if the user rapdily zooms through the available levels, they'll have a smoother experience if you only load
        // the data once they've stopped zooming
        refreshMapTimer.Stop();
        refreshMapTimer.Start();
      }
    }

    private void RefreshMapTimer_Tick(object sender, EventArgs e)
    {
      refreshMapTimer.Stop();
      UpdateMap();
    }

    private void İmageBox_MouseDown(object sender, MouseEventArgs e)
    {
      //var point = e.Location;

      //var realPoint = imageBox.PointToImage(point);
      //BilgiGoster(realPoint, "Test", "Şehir", Resources.MapMarker);
    }

    private void BilgiGoster(Point point, string name, string type, Image image)
    {
      if (bilgiForm == null || bilgiForm.IsDisposed)
      {
        bilgiForm = new Bilgilendirme(point, name, type, image);
        BilgiGosterTasi(Cursor.Position.X, Cursor.Position.Y);
        bilgiForm.Show();
      }
      else
      {
        bilgiForm.Isim = name;
        bilgiForm.Konum = point;
        bilgiForm.Resim = image;
        bilgiForm.Tur = type;
        BilgiGosterTasi(Cursor.Position.X, Cursor.Position.Y);
      }
    }

    private void BilgiGosterTasi(int x, int y)
    {
      if (bilgiForm != null && !bilgiForm.IsDisposed) bilgiForm.SetDesktopLocation(x + 10, y + 10);
    }

    private void İmageBox_MouseMove(object sender, MouseEventArgs e)
    {
      BilgiGosterTasi(Cursor.Position.X, Cursor.Position.Y);

      var realPoint = imageBox.PointToImage(e.Location);

      var landmark = Neresi(realPoint);
      if (landmark != null)
      {
        BilgiGoster(landmark.LocationPoint, landmark.Isim, landmark.Tur, Resources.MapMarker);
      }
      else
      {
        if (bilgiForm != null && !bilgiForm.IsDisposed) bilgiForm.Close();
      }
    }

    private ImagePoint Neresi(Point realPoint)
    {
      foreach (var landmark in landmarks)
      {
        var location = realPoint;

        var findNearestLayer = FindNearestLayer(VirtualZoom);
        if (LayerData.Count <= findNearestLayer || findNearestLayer >= 0)
        {
          var p = new Point((int) landmark.location.X, (int) landmark.location.Y);

          var div = LayerData[findNearestLayer].ZoomLevel;
          p.X *= div;
          p.Y *= div;

          if ((p.X - location.X) * (p.X - location.X) + (p.Y - location.Y) * (p.Y - location.Y) <=
              500)
            return landmark;
        }
      }

      return null;
    }

    private void İmageBox_MouseLeave(object sender, EventArgs e)
    {
      if (bilgiForm != null && !bilgiForm.IsDisposed) bilgiForm.Close();
    }

    private struct MapLayerData
    {
      #region Public Properties

      public int LowerZoom { get; set; }

      public string Name { get; set; }

      public int UpperZoom { get; set; }

      public int ZoomLevel { get; set; }

      #endregion
    }
  }
}