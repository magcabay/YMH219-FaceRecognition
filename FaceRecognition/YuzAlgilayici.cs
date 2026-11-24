using OpenCvSharp;

namespace FaceRecognition
{
    // Haar Cascade ile görüntüdeki yüzleri bulan sınıf
    public class YuzAlgilayici : IDisposable
    {
        private readonly CascadeClassifier siniflandirici;

        public YuzAlgilayici(string cascadeDosyasi)
        {
            if (!File.Exists(cascadeDosyasi))
                throw new FileNotFoundException("Yüz algılama model dosyası bulunamadı.", cascadeDosyasi);

            siniflandirici = new CascadeClassifier(cascadeDosyasi);
            if (siniflandirici.Empty())
                throw new InvalidDataException("Cascade dosyası okunamadı veya bozuk: " + cascadeDosyasi);
        }

        public Rect[] YuzleriBul(Mat renkliGoruntu)
        {
            using (Mat gri = new Mat())
            {
                Cv2.CvtColor(renkliGoruntu, gri, ColorConversionCodes.BGR2GRAY);
                Cv2.EqualizeHist(gri, gri);

                return siniflandirici.DetectMultiScale(
                    gri,
                    scaleFactor: 1.1,
                    minNeighbors: 5,
                    flags: HaarDetectionTypes.ScaleImage,
                    minSize: new OpenCvSharp.Size(60, 60));
            }
        }

        // tanıma için yüz bölgesini kesip gri ve sabit boyutlu hale getirir
        public static Mat YuzuHazirla(Mat renkliGoruntu, Rect yuz)
        {
            using (Mat kesit = new Mat(renkliGoruntu, yuz))
            using (Mat gri = new Mat())
            {
                Cv2.CvtColor(kesit, gri, ColorConversionCodes.BGR2GRAY);
                Mat sonuc = new Mat();
                Cv2.Resize(gri, sonuc, new OpenCvSharp.Size(YuzTanimaModeli.YuzBoyutu, YuzTanimaModeli.YuzBoyutu));
                Cv2.EqualizeHist(sonuc, sonuc);
                return sonuc;
            }
        }

        public void Dispose()
        {
            siniflandirici?.Dispose();
        }
    }
}
