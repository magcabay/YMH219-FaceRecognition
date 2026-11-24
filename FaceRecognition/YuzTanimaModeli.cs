using OpenCvSharp;
using OpenCvSharp.Face;

namespace FaceRecognition
{
    // Tahmin sonucu
    public class TahminSonucu
    {
        public string Etiket { get; set; }
        public double Mesafe { get; set; }      // LBPH'de küçük değer = daha benzer
        public bool Tanindi { get; set; }

        // kullanıcıya göstermek için kabaca 0-100 arası bir güven skoru
        public int GuvenYuzdesi
        {
            get { return (int)Math.Max(0, Math.Min(100, 100 - Mesafe)); }
        }
    }

    // LBPH yüz tanıyıcıyı ve yerel veri setini yöneten sınıf.
    // Veri seti klasör yapısı:  VeriSeti/<etiket>/<resim>.png
    public class YuzTanimaModeli : IDisposable
    {
        public const int YuzBoyutu = 100;

        // bu mesafenin üstündeki tahminleri "Bilinmiyor" sayıyoruz (deneyerek bulduk)
        public double EsikDegeri { get; set; } = 80;

        private LBPHFaceRecognizer tanimayici;
        private readonly Dictionary<int, string> etiketler = new Dictionary<int, string>();

        public string VeriKlasoru { get; }
        public bool Egitildi { get; private set; }

        public YuzTanimaModeli(string veriKlasoru)
        {
            VeriKlasoru = veriKlasoru;
            Directory.CreateDirectory(VeriKlasoru);
        }

        public void OrnekKaydet(Mat hazirYuz, string etiket)
        {
            if (string.IsNullOrWhiteSpace(etiket))
                throw new ArgumentException("Etiket (kişi adı) boş olamaz.");

            // klasör adında sorun çıkaracak karakterleri temizle
            foreach (char c in Path.GetInvalidFileNameChars())
                etiket = etiket.Replace(c, '_');

            string klasor = Path.Combine(VeriKlasoru, etiket.Trim());
            Directory.CreateDirectory(klasor);

            string dosya = Path.Combine(klasor, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".png");
            Cv2.ImWrite(dosya, hazirYuz);
        }

        // etiket -> örnek sayısı
        public Dictionary<string, int> OrnekSayilari()
        {
            Dictionary<string, int> sayilar = new Dictionary<string, int>();
            foreach (string klasor in Directory.GetDirectories(VeriKlasoru))
            {
                int adet = Directory.GetFiles(klasor, "*.png").Length;
                if (adet > 0)
                    sayilar[Path.GetFileName(klasor)] = adet;
            }
            return sayilar;
        }

        // veri setindeki tüm resimlerle modeli eğitir, kullanılan resim sayısını döner
        public int Egit()
        {
            List<Mat> resimler = new List<Mat>();
            List<int> numaralar = new List<int>();
            etiketler.Clear();

            try
            {
                int numara = 0;
                foreach (string klasor in Directory.GetDirectories(VeriKlasoru))
                {
                    string[] dosyalar = Directory.GetFiles(klasor, "*.png");
                    if (dosyalar.Length == 0) continue;

                    etiketler[numara] = Path.GetFileName(klasor);

                    foreach (string dosya in dosyalar)
                    {
                        Mat resim = Cv2.ImRead(dosya, ImreadModes.Grayscale);
                        if (resim.Empty())
                        {
                            resim.Dispose();
                            continue;
                        }
                        if (resim.Width != YuzBoyutu || resim.Height != YuzBoyutu)
                            Cv2.Resize(resim, resim, new OpenCvSharp.Size(YuzBoyutu, YuzBoyutu));

                        resimler.Add(resim);
                        numaralar.Add(numara);
                    }
                    numara++;
                }

                if (resimler.Count == 0)
                    throw new InvalidOperationException("Veri setinde hiç örnek yok. Önce 'Örnek Ekle' ile yüz örnekleri kaydedin.");

                tanimayici?.Dispose();
                tanimayici = LBPHFaceRecognizer.Create();
                tanimayici.Train(resimler, numaralar);
                Egitildi = true;

                return resimler.Count;
            }
            finally
            {
                foreach (Mat m in resimler)
                    m.Dispose();
            }
        }

        public int EtiketSayisi
        {
            get { return etiketler.Count; }
        }

        public TahminSonucu Tahmin(Mat hazirYuz)
        {
            if (!Egitildi)
                return new TahminSonucu { Etiket = "Model eğitilmedi", Mesafe = 0, Tanindi = false };

            tanimayici.Predict(hazirYuz, out int numara, out double mesafe);

            bool tanindi = mesafe <= EsikDegeri && etiketler.ContainsKey(numara);
            return new TahminSonucu
            {
                Etiket = tanindi ? etiketler[numara] : "Bilinmiyor",
                Mesafe = mesafe,
                Tanindi = tanindi
            };
        }

        public void Dispose()
        {
            tanimayici?.Dispose();
        }
    }
}
