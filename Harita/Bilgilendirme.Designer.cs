namespace Harita
{
  partial class Bilgilendirme
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
      this.label1 = new System.Windows.Forms.Label();
      this.KonumTextBox = new System.Windows.Forms.TextBox();
      this.TurTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.IsimTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.ResimImageBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.ResimImageBox)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(7, 164);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 22);
      this.label1.TabIndex = 0;
      this.label1.Text = "Konum";
      // 
      // KonumTextBox
      // 
      this.KonumTextBox.Location = new System.Drawing.Point(113, 164);
      this.KonumTextBox.Name = "KonumTextBox";
      this.KonumTextBox.ReadOnly = true;
      this.KonumTextBox.Size = new System.Drawing.Size(210, 22);
      this.KonumTextBox.TabIndex = 1;
      // 
      // TurTextBox
      // 
      this.TurTextBox.Location = new System.Drawing.Point(113, 220);
      this.TurTextBox.Name = "TurTextBox";
      this.TurTextBox.ReadOnly = true;
      this.TurTextBox.Size = new System.Drawing.Size(210, 22);
      this.TurTextBox.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(7, 220);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 22);
      this.label2.TabIndex = 2;
      this.label2.Text = "Tür";
      // 
      // IsimTextBox
      // 
      this.IsimTextBox.Location = new System.Drawing.Point(113, 192);
      this.IsimTextBox.Name = "IsimTextBox";
      this.IsimTextBox.ReadOnly = true;
      this.IsimTextBox.Size = new System.Drawing.Size(210, 22);
      this.IsimTextBox.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(7, 192);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 22);
      this.label3.TabIndex = 4;
      this.label3.Text = "İsim";
      // 
      // ResimImageBox
      // 
      this.ResimImageBox.Location = new System.Drawing.Point(12, 12);
      this.ResimImageBox.Name = "ResimImageBox";
      this.ResimImageBox.Size = new System.Drawing.Size(311, 130);
      this.ResimImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.ResimImageBox.TabIndex = 6;
      this.ResimImageBox.TabStop = false;
      // 
      // Bilgilendirme
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(336, 263);
      this.Controls.Add(this.ResimImageBox);
      this.Controls.Add(this.IsimTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.TurTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.KonumTextBox);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "Bilgilendirme";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Bilgilendirme";
      this.TopMost = true;
      ((System.ComponentModel.ISupportInitialize)(this.ResimImageBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox KonumTextBox;
    private System.Windows.Forms.TextBox TurTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox IsimTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.PictureBox ResimImageBox;
  }
}