namespace FaceRecognition
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            picGoruntu = new PictureBox();
            grpKaynak = new GroupBox();
            btnResimSec = new Button();
            btnKameraDurdur = new Button();
            btnKameraBaslat = new Button();
            grpSonuc = new GroupBox();
            lblYuzSayisi = new Label();
            lblGuven = new Label();
            lblEtiket = new Label();
            grpEgitim = new GroupBox();
            lstEtiketler = new ListBox();
            lblVeriSeti = new Label();
            btnEgit = new Button();
            btnOrnekEkle = new Button();
            txtEtiket = new TextBox();
            lblEtiketAdi = new Label();
            statusStrip1 = new StatusStrip();
            lblDurum = new ToolStripStatusLabel();
            kameraTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)picGoruntu).BeginInit();
            grpKaynak.SuspendLayout();
            grpSonuc.SuspendLayout();
            grpEgitim.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            //
            // picGoruntu
            //
            picGoruntu.BackColor = Color.Black;
            picGoruntu.BorderStyle = BorderStyle.FixedSingle;
            picGoruntu.Location = new Point(12, 12);
            picGoruntu.Name = "picGoruntu";
            picGoruntu.Size = new Size(640, 480);
            picGoruntu.SizeMode = PictureBoxSizeMode.Zoom;
            picGoruntu.TabIndex = 0;
            picGoruntu.TabStop = false;
            //
            // grpKaynak
            //
            grpKaynak.Controls.Add(btnResimSec);
            grpKaynak.Controls.Add(btnKameraDurdur);
            grpKaynak.Controls.Add(btnKameraBaslat);
            grpKaynak.Location = new Point(664, 8);
            grpKaynak.Name = "grpKaynak";
            grpKaynak.Size = new Size(280, 104);
            grpKaynak.TabIndex = 1;
            grpKaynak.TabStop = false;
            grpKaynak.Text = "Görüntü Kaynağı";
            //
            // btnResimSec
            //
            btnResimSec.Location = new Point(12, 62);
            btnResimSec.Name = "btnResimSec";
            btnResimSec.Size = new Size(256, 30);
            btnResimSec.TabIndex = 2;
            btnResimSec.Text = "Görüntü Seç (dosyadan)";
            btnResimSec.UseVisualStyleBackColor = true;
            btnResimSec.Click += btnResimSec_Click;
            //
            // btnKameraDurdur
            //
            btnKameraDurdur.Enabled = false;
            btnKameraDurdur.Location = new Point(142, 24);
            btnKameraDurdur.Name = "btnKameraDurdur";
            btnKameraDurdur.Size = new Size(126, 30);
            btnKameraDurdur.TabIndex = 1;
            btnKameraDurdur.Text = "Kamera Durdur";
            btnKameraDurdur.UseVisualStyleBackColor = true;
            btnKameraDurdur.Click += btnKameraDurdur_Click;
            //
            // btnKameraBaslat
            //
            btnKameraBaslat.Location = new Point(12, 24);
            btnKameraBaslat.Name = "btnKameraBaslat";
            btnKameraBaslat.Size = new Size(126, 30);
            btnKameraBaslat.TabIndex = 0;
            btnKameraBaslat.Text = "Kamera Başlat";
            btnKameraBaslat.UseVisualStyleBackColor = true;
            btnKameraBaslat.Click += btnKameraBaslat_Click;
            //
            // grpSonuc
            //
            grpSonuc.Controls.Add(lblYuzSayisi);
            grpSonuc.Controls.Add(lblGuven);
            grpSonuc.Controls.Add(lblEtiket);
            grpSonuc.Location = new Point(664, 118);
            grpSonuc.Name = "grpSonuc";
            grpSonuc.Size = new Size(280, 118);
            grpSonuc.TabIndex = 2;
            grpSonuc.TabStop = false;
            grpSonuc.Text = "Tanıma Sonucu";
            //
            // lblYuzSayisi
            //
            lblYuzSayisi.AutoSize = true;
            lblYuzSayisi.Location = new Point(12, 90);
            lblYuzSayisi.Name = "lblYuzSayisi";
            lblYuzSayisi.Size = new Size(110, 15);
            lblYuzSayisi.TabIndex = 2;
            lblYuzSayisi.Text = "Algılanan yüz sayısı: 0";
            //
            // lblGuven
            //
            lblGuven.AutoSize = true;
            lblGuven.Location = new Point(12, 64);
            lblGuven.Name = "lblGuven";
            lblGuven.Size = new Size(60, 15);
            lblGuven.TabIndex = 1;
            lblGuven.Text = "Güven: -";
            //
            // lblEtiket
            //
            lblEtiket.AutoSize = true;
            lblEtiket.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblEtiket.ForeColor = Color.DimGray;
            lblEtiket.Location = new Point(10, 26);
            lblEtiket.Name = "lblEtiket";
            lblEtiket.Size = new Size(26, 25);
            lblEtiket.TabIndex = 0;
            lblEtiket.Text = "-";
            //
            // grpEgitim
            //
            grpEgitim.Controls.Add(lstEtiketler);
            grpEgitim.Controls.Add(lblVeriSeti);
            grpEgitim.Controls.Add(btnEgit);
            grpEgitim.Controls.Add(btnOrnekEkle);
            grpEgitim.Controls.Add(txtEtiket);
            grpEgitim.Controls.Add(lblEtiketAdi);
            grpEgitim.Location = new Point(664, 242);
            grpEgitim.Name = "grpEgitim";
            grpEgitim.Size = new Size(280, 250);
            grpEgitim.TabIndex = 3;
            grpEgitim.TabStop = false;
            grpEgitim.Text = "Veri Ekleme / Eğitim";
            //
            // lstEtiketler
            //
            lstEtiketler.FormattingEnabled = true;
            lstEtiketler.IntegralHeight = false;
            lstEtiketler.Location = new Point(12, 148);
            lstEtiketler.Name = "lstEtiketler";
            lstEtiketler.Size = new Size(256, 90);
            lstEtiketler.TabIndex = 5;
            lstEtiketler.SelectedIndexChanged += lstEtiketler_SelectedIndexChanged;
            //
            // lblVeriSeti
            //
            lblVeriSeti.AutoSize = true;
            lblVeriSeti.Location = new Point(12, 130);
            lblVeriSeti.Name = "lblVeriSeti";
            lblVeriSeti.Size = new Size(150, 15);
            lblVeriSeti.TabIndex = 4;
            lblVeriSeti.Text = "Veri seti (etiket: örnek sayısı):";
            //
            // btnEgit
            //
            btnEgit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEgit.Location = new Point(12, 92);
            btnEgit.Name = "btnEgit";
            btnEgit.Size = new Size(256, 30);
            btnEgit.TabIndex = 3;
            btnEgit.Text = "Modeli Eğit";
            btnEgit.UseVisualStyleBackColor = true;
            btnEgit.Click += btnEgit_Click;
            //
            // btnOrnekEkle
            //
            btnOrnekEkle.Location = new Point(12, 56);
            btnOrnekEkle.Name = "btnOrnekEkle";
            btnOrnekEkle.Size = new Size(256, 30);
            btnOrnekEkle.TabIndex = 2;
            btnOrnekEkle.Text = "Görüntüdeki Yüzü Örnek Olarak Ekle";
            btnOrnekEkle.UseVisualStyleBackColor = true;
            btnOrnekEkle.Click += btnOrnekEkle_Click;
            //
            // txtEtiket
            //
            txtEtiket.Location = new Point(102, 24);
            txtEtiket.MaxLength = 30;
            txtEtiket.Name = "txtEtiket";
            txtEtiket.PlaceholderText = "ör: Ali";
            txtEtiket.Size = new Size(166, 23);
            txtEtiket.TabIndex = 1;
            //
            // lblEtiketAdi
            //
            lblEtiketAdi.AutoSize = true;
            lblEtiketAdi.Location = new Point(12, 27);
            lblEtiketAdi.Name = "lblEtiketAdi";
            lblEtiketAdi.Size = new Size(84, 15);
            lblEtiketAdi.TabIndex = 0;
            lblEtiketAdi.Text = "Etiket (kişi adı):";
            //
            // statusStrip1
            //
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDurum });
            statusStrip1.Location = new Point(0, 502);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(956, 22);
            statusStrip1.TabIndex = 4;
            //
            // lblDurum
            //
            lblDurum.Name = "lblDurum";
            lblDurum.Size = new Size(41, 17);
            lblDurum.Text = "Hazır.";
            //
            // kameraTimer
            //
            kameraTimer.Interval = 40;
            kameraTimer.Tick += kameraTimer_Tick;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(956, 524);
            Controls.Add(statusStrip1);
            Controls.Add(grpEgitim);
            Controls.Add(grpSonuc);
            Controls.Add(grpKaynak);
            Controls.Add(picGoruntu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OpenCV Yüz Algılama ve Tanıma - YMH219";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)picGoruntu).EndInit();
            grpKaynak.ResumeLayout(false);
            grpSonuc.ResumeLayout(false);
            grpSonuc.PerformLayout();
            grpEgitim.ResumeLayout(false);
            grpEgitim.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picGoruntu;
        private GroupBox grpKaynak;
        private Button btnResimSec;
        private Button btnKameraDurdur;
        private Button btnKameraBaslat;
        private GroupBox grpSonuc;
        private Label lblYuzSayisi;
        private Label lblGuven;
        private Label lblEtiket;
        private GroupBox grpEgitim;
        private ListBox lstEtiketler;
        private Label lblVeriSeti;
        private Button btnEgit;
        private Button btnOrnekEkle;
        private TextBox txtEtiket;
        private Label lblEtiketAdi;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblDurum;
        private System.Windows.Forms.Timer kameraTimer;
    }
}
