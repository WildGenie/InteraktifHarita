namespace Harita
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.imageBox = new Cyotek.Windows.Forms.ImageBox();
      this.refreshMapTimer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // imageBox
      // 
      this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.imageBox.Location = new System.Drawing.Point(0, 0);
      this.imageBox.Margin = new System.Windows.Forms.Padding(4);
      this.imageBox.Name = "imageBox";
      this.imageBox.Size = new System.Drawing.Size(1067, 554);
      this.imageBox.TabIndex = 0;
      this.imageBox.Zoomed += new System.EventHandler<Cyotek.Windows.Forms.ImageBoxZoomEventArgs>(this.ImageBox_Zoomed);
      this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageBox_Paint);
      // 
      // refreshMapTimer
      // 
      this.refreshMapTimer.Interval = 5;
      this.refreshMapTimer.Tick += new System.EventHandler(this.RefreshMapTimer_Tick);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1067, 554);
      this.Controls.Add(this.imageBox);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

        #endregion

        private Cyotek.Windows.Forms.ImageBox imageBox;
    private System.Windows.Forms.Timer refreshMapTimer;
  }
}

