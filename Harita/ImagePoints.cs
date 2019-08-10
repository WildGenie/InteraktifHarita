using System;
using System.Drawing;
using Cyotek.Windows.Forms;

namespace Harita
{
  public static class GlobalSettings
  {
    public static Pen crossPen = new Pen(Color.Red);
    public static Pen profilPen = new Pen(Color.Yellow, 1.5f);
    public static Pen calibratePen = new Pen(Color.Red, 2);
    public static Pen selectedPen = new Pen(Color.Fuchsia, 2);
    public static Pen linesPen = new Pen(Color.Green, 2);
    public static Font fontLines = new Font("Thaoma", 11);
    public static Pen indexBrush = new Pen(Color.Green, 2);
    public static Font fontDescription = new Font("Thaoma", 11);
    public static Pen descriptionBrush = new Pen(Color.Red, 2);
    public static Font fontPoints = new Font("Thaoma", 11);
    public static int lineLength = 20;

    public static int roundTime = 4;
    public static int roundPitchPlunge = 2;
    public static int roundOthers = 2;

    public static bool desc = true;
    public static bool points = true;
    public static bool pointsDesc = true;
    public static bool lines = true;
    public static bool linesDesc = true;
    public static bool lineFringeNumber = true;
    public static bool profil;

    // ratio = distancebetweencalibrationpoints/realdistance [px/mm]
    public static double ratio = double.NaN;

    public static bool fringeLabels = false;
    public static bool fringeLabelsPanel = false;
    public static int fringeCircleSize = 3;
    public static float fringeStep = 1;
    public static Color fringeLabelsColor = Color.Green;
    public static Font fringeLabelsFont = new Font("Thaoma", 11);
    public static float fringeLabelsFrom = 0;
    public static float fringeLabelsTo = 1;
  }

  [Serializable]
  public class ImagePoint
  {
    public string color;
    public bool exist = true;
    public string label;
    public PointF labelLocation;
    public float labelOffsetX = 0;
    public float labelOffsetY = -30;
    public PointF location;
    [NonSerialized] public Pen pen;

    public float ratio = 10;
    public SizeF sizeString;

    public ImagePoint(string isim, string tur)
    {
      Isim = isim;
      Tur = tur;
    }

    public ImagePoint(string name, PointF l, string tur)
    {
      location = l;
      Isim = name;
      Tur = tur;
      label = name;
    }

    public ImagePoint(Pen p, PointF l)
    {
      pen = p;
      location = l;
    }

    public Point LocationPoint => new Point((int) location.X, (int) location.Y);

    public string Isim { get; set; }

    public string Tur { get; set; }

    public Image Resim { get; set; }

    public void DrawToGraphics(Graphics g, PointF center, bool isFringe = false, bool isSelected = false)
    {
      if (isFringe)
      {
        if (isSelected)
          g.DrawEllipse(new Pen(GlobalSettings.selectedPen.Color, 1.5f), center.X - GlobalSettings.fringeCircleSize,
            center.Y - GlobalSettings.fringeCircleSize, GlobalSettings.fringeCircleSize * 2,
            GlobalSettings.fringeCircleSize * 2);
        else
          g.DrawEllipse(new Pen(GlobalSettings.fringeLabelsColor, 1.5f), center.X - GlobalSettings.fringeCircleSize,
            center.Y - GlobalSettings.fringeCircleSize, GlobalSettings.fringeCircleSize * 2,
            GlobalSettings.fringeCircleSize * 2);
      }
      else
      {
        g.DrawEllipse(pen, center.X - ratio, center.Y - ratio, ratio * 2, ratio * 2);
      }
    }

    public void DrawToGraphics(Graphics g, PointF center, int n)
    {
      g.DrawEllipse(pen, center.X - ratio * n, center.Y - ratio * n, ratio * n * 2, ratio * n * 2);
    }

    public PointF GetLabelLocation(ImageBox i)
    {
      return new PointF(i.GetOffsetPoint(new PointF(location.X + labelOffsetX, location.Y + labelOffsetY)).X,
        i.GetOffsetPoint(new PointF(location.X + labelOffsetX, location.Y + labelOffsetY)).Y +
        (float) (8 * ((i.ZoomFactor - 0.7) / 0.3)));
    }

    public PointF GetLabelLocation()
    {
      return new PointF(location.X + labelOffsetX, location.Y + labelOffsetY);
    }

    public bool IsHitLabel(PointF p, ImageBox i, bool isFringeLabel = false)
    {
      PointF offsetPoint;
      if (isFringeLabel)
      {
        var size = i.CreateGraphics().MeasureString(label, GlobalSettings.fringeLabelsFont);
        var l = i.GetOffsetPoint(location);
        //click = i.GetOffsetPoint(p);
        offsetPoint = i.GetOffsetPoint(new PointF(location.X + labelOffsetX, location.Y + labelOffsetY));
        if (p.X < offsetPoint.X + size.Width
            && p.X > offsetPoint.X - 4
            && p.Y < offsetPoint.Y + size.Height + (float) (8 * ((i.ZoomFactor - 0.7) / 0.3))
            && p.Y > offsetPoint.Y + (float) (8 * ((i.ZoomFactor - 0.7) / 0.3)))
          return true;
        return false;
      }

      p = i.GetOffsetPoint(p);
      offsetPoint = i.GetOffsetPoint(new PointF(location.X + labelOffsetX, location.Y + labelOffsetY));
      if (p.X < offsetPoint.X + sizeString.Width
          && p.X > offsetPoint.X
          && p.Y < offsetPoint.Y + sizeString.Height + (float) (8 * ((i.ZoomFactor - 0.7) / 0.3))
          && p.Y > offsetPoint.Y + (float) (8 * ((i.ZoomFactor - 0.7) / 0.3)))
        return true;
      return false;
    }

    public bool IsHit(Point p, ImageBox i, int div, bool isFringe = false)
    {
      if (isFringe)
      {
        p = i.PointToImage(p);
        p.X /= div;
        p.Y /= div;
        if ((p.X - location.X) * (p.X - location.X) + (p.Y - location.Y) * (p.Y - location.Y) <=
            ratio / div * (ratio / div))
          return true;
        return false;
      }

      p = i.GetOffsetPoint(p);
      var offsetPoint = i.GetOffsetPoint(location);
      if ((p.X - offsetPoint.X) * (p.X - offsetPoint.X) + (p.Y - offsetPoint.Y) * (p.Y - offsetPoint.Y) <=
          ratio * ratio)
        return true;
      return false;
    }
  }
}