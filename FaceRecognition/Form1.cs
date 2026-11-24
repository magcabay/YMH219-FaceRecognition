using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace FaceRecognition
{
    public partial class Form1 : Form
    {
        private const string CascadeDosyaAdi = "haarcascade_frontalface_default.xml";

        private YuzAlgilayici algilayici;
        private YuzTanimaModeli model;
        private readonly Kamera kamera = new Kamera();

        // "Örnek Ekle" için ekrandaki son ham görüntü
        private Mat sonGoruntu;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cascadeYolu = Path.Combine(AppContext.BaseDirectory, CascadeDosyaAdi);
            try
            {
                algilayici = new YuzAlgilayici(cascadeYolu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Yüz algılama modeli yüklenemedi!\n\n" + ex.Message +
                    $"\n\n'{CascadeDosyaAdi}' dosyasını proje klasörüne koyup yeniden derleyin (README'ye bakın).",
                    "Model Dosyası Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DurumYaz("HATA: Cascade dosyası bulunamadı, yüz algılama devre dışı.");
                btnOrnekEkle.Enabled = false;
                btnEgit.Enabled = false;
            }

            model = new YuzTanimaModeli(Path.Combine(AppContext.BaseDirectory, "VeriSeti"));
            VeriSetiListesiniGuncelle();

            // daha önce kaydedilmiş örnek varsa açılışta eğit
            if (algilayici != null && model.OrnekSayilari().Count > 0)
            {
                try
                {
                    int adet = model.Egit();
                    DurumYaz($"Kayıtlı veri setiyle model eğitildi ({adet} örnek, {model.EtiketSayisi} etiket).");
                }
                catch (Exception ex)
                {
                    DurumYaz("Model eğitilemedi: " + ex.Message);
                }
            }
        }

        #region Kamera ve resim

        private void btnKameraBaslat_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                kamera.Ac(0);
                kameraTimer.Start();
                btnKameraBaslat.Enabled = false;
                btnKameraDurdur.Enabled = true;
                DurumYaz("Kamera çalışıyor.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kamera Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DurumYaz("Kamera açılamadı.");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnKameraDurdur_Click(object sender, EventArgs e)
        {
            KamerayiDurdur();
        }

        private void KamerayiDurdur()
        {
            kameraTimer.Stop();
            kamera.Kapat();
            btnKameraBaslat.Enabled = true;
            btnKameraDurdur.Enabled = false;
            DurumYaz("Kamera durduruldu.");
        }

        private void kameraTimer_Tick(object sender, EventArgs e)
        {
            Mat kare = kamera.KareOku();
            if (kare == null)
            {
                KamerayiDurdur();
                MessageBox.Show("Kameradan görüntü alınamıyor. Bağlantı kopmuş olabilir.", "Kamera Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GoruntuyuIsle(kare);
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Yüz içeren bir resim seçin";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                if (kamera.Acik)
                    KamerayiDurdur();

                Mat resim = Cv2.ImRead(ofd.FileName, ImreadModes.Color);
                if (resim.Empty())
                {
                    resim.Dispose();
                    MessageBox.Show("Resim okunamadı. Dosya bozuk ya da desteklenmeyen bir formatta olabilir.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                GoruntuyuIsle(resim);
                DurumYaz("Görüntü yüklendi: " + Path.GetFileName(ofd.FileName));
            }
        }

        #endregion

        // yüzleri bulur, varsa tanır, dikdörtgen çizip PictureBox'ta gösterir
        private void GoruntuyuIsle(Mat goruntu)
        {
            sonGoruntu?.Dispose();
            sonGoruntu = goruntu.Clone();

            Rect[] yuzler = algilayici != null ? algilayici.YuzleriBul(goruntu) : Array.Empty<Rect>();
            lblYuzSayisi.Text = "Algılanan yüz sayısı: " + yuzler.Length;

            TahminSonucu ilkSonuc = null;

            foreach (Rect yuz in yuzler)
            {
                string yazi = "Yuz";
                Scalar renk = Scalar.Yellow;

                if (model.Egitildi)
                {
                    using (Mat hazir = YuzAlgilayici.YuzuHazirla(goruntu, yuz))
                    {
                        TahminSonucu sonuc = model.Tahmin(hazir);
                        if (ilkSonuc == null) ilkSonuc = sonuc;

                        // OpenCV PutText Türkçe karakter gösteremiyor, sadeleştiriyoruz
                        yazi = TurkceKarakterTemizle(sonuc.Etiket) + $" ({sonuc.Mesafe:0})";
                        renk = sonuc.Tanindi ? Scalar.LimeGreen : Scalar.Red;
                    }
                }

                Cv2.Rectangle(goruntu, yuz, renk, 2);
                Cv2.PutText(goruntu, yazi, new OpenCvSharp.Point(yuz.X, Math.Max(20, yuz.Y - 8)),
                    HersheyFonts.HersheySimplex, 0.7, renk, 2);
            }

            SonucuGoster(yuzler.Length, ilkSonuc);
            ResmiGoster(goruntu);
            goruntu.Dispose();
        }

        private void SonucuGoster(int yuzSayisi, TahminSonucu sonuc)
        {
            if (yuzSayisi == 0)
            {
                lblEtiket.Text = "Yüz yok";
                lblEtiket.ForeColor = Color.DimGray;
                lblGuven.Text = "Güven: -";
            }
            else if (sonuc == null)
            {
                lblEtiket.Text = "Yüz algılandı";
                lblEtiket.ForeColor = Color.DarkGoldenrod;
                lblGuven.Text = "Güven: - (model eğitilmedi)";
            }
            else
            {
                lblEtiket.Text = sonuc.Etiket;
                lblEtiket.ForeColor = sonuc.Tanindi ? Color.SeaGreen : Color.IndianRed;
                lblGuven.Text = $"Güven: %{sonuc.GuvenYuzdesi}   (LBPH mesafe: {sonuc.Mesafe:0.0})";
            }
        }

        private void ResmiGoster(Mat goruntu)
        {
            Image eski = picGoruntu.Image;
            picGoruntu.Image = BitmapConverter.ToBitmap(goruntu);
            eski?.Dispose(); // bellek şişmesin
        }

        #region Eğitim

        private void btnOrnekEkle_Click(object sender, EventArgs e)
        {
            string etiket = txtEtiket.Text.Trim();
            if (etiket == "")
            {
                MessageBox.Show("Önce bir etiket (kişi adı) yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEtiket.Focus();
                return;
            }
            if (sonGoruntu == null || sonGoruntu.Empty())
            {
                MessageBox.Show("Önce kamerayı başlatın veya bir resim seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Rect[] yuzler = algilayici.YuzleriBul(sonGoruntu);
            if (yuzler.Length == 0)
            {
                MessageBox.Show("Görüntüde yüz bulunamadı. Kameraya daha net bakmayı deneyin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // birden fazla yüz varsa en büyüğünü (kameraya en yakın) al
            Rect enBuyuk = yuzler.OrderByDescending(r => r.Width * r.Height).First();

            try
            {
                using (Mat hazir = YuzAlgilayici.YuzuHazirla(sonGoruntu, enBuyuk))
                {
                    model.OrnekKaydet(hazir, etiket);
                }
                VeriSetiListesiniGuncelle();
                DurumYaz($"'{etiket}' için örnek kaydedildi. Bittiğinde 'Modeli Eğit'e basın.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Örnek kaydedilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEgit_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> sayilar = model.OrnekSayilari();
            if (sayilar.Count < 2)
            {
                DialogResult cevap = MessageBox.Show(
                    "Anlamlı bir tanıma için en az 2 farklı etiket önerilir. Yine de eğitilsin mi?",
                    "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap != DialogResult.Yes) return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                int adet = model.Egit();
                DurumYaz($"Model eğitildi: {adet} örnek, {model.EtiketSayisi} etiket.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Model eğitilemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void lstEtiketler_SelectedIndexChanged(object sender, EventArgs e)
        {
            // listeden seçince etiketi kutuya yaz, aynı kişiye örnek eklemek kolay olsun
            if (lstEtiketler.SelectedItem != null)
                txtEtiket.Text = lstEtiketler.SelectedItem.ToString().Split(':')[0].Trim();
        }

        private void VeriSetiListesiniGuncelle()
        {
            lstEtiketler.Items.Clear();
            foreach (var kayit in model.OrnekSayilari())
                lstEtiketler.Items.Add($"{kayit.Key} : {kayit.Value} örnek");
        }

        #endregion

        private static string TurkceKarakterTemizle(string s)
        {
            return s.Replace('ı', 'i').Replace('İ', 'I').Replace('ş', 's').Replace('Ş', 'S')
                    .Replace('ğ', 'g').Replace('Ğ', 'G').Replace('ü', 'u').Replace('Ü', 'U')
                    .Replace('ö', 'o').Replace('Ö', 'O').Replace('ç', 'c').Replace('Ç', 'C');
        }

        private void DurumYaz(string metin)
        {
            lblDurum.Text = $"[{DateTime.Now:HH:mm:ss}] {metin}";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kameraTimer.Stop();
            kamera.Dispose();
            algilayici?.Dispose();
            model?.Dispose();
            sonGoruntu?.Dispose();
        }
    }
}
