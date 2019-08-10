using System.Drawing;
using System.Windows.Forms;

namespace Harita
{
  public partial class Bilgilendirme : Form
  {
    private string isim;
    private Point konum;
    private Image resim;
    private string tur;

    public Bilgilendirme(Point konum, string isim, string tur, Image resim)
    {
      InitializeComponent();
      Konum = konum;
      Isim = isim;
      Tur = tur;
      Resim = resim;
    }

    public Point Konum
    {
      get => konum;
      set
      {
        konum = value;
        //KonumTextBox.Text = konum.X + ", " + konum.X;
        //KonumTextBox.Text = string.Format("{0}, {1}", konum.X, konum.Y);
        KonumTextBox.Text = $"{konum.X}, {konum.Y}";
        // printf("%d, %d" , konumx, konumy);
      }
    }

    public string Isim
    {
      get => isim;
      set
      {
        isim = value;
        IsimTextBox.Text = isim;
      }
    }

    public string Tur
    {
      get => tur;
      set
      {
        tur = value;
        TurTextBox.Text = tur;
      }
    }

    public Image Resim
    {
      get => resim;
      set
      {
        resim = value;
        ResimImageBox.Image = resim;
      }
    }
  }
}