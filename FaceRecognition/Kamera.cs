using OpenCvSharp;

namespace FaceRecognition
{
    // Web kamerasından kare okuyan basit sınıf
    public class Kamera : IDisposable
    {
        private VideoCapture yakalayici;

        public bool Acik
        {
            get { return yakalayici != null && yakalayici.IsOpened(); }
        }

        public void Ac(int kameraNo = 0)
        {
            Kapat();
            yakalayici = new VideoCapture(kameraNo, VideoCaptureAPIs.DSHOW);
            if (!yakalayici.IsOpened())
            {
                yakalayici.Dispose();
                yakalayici = null;
                throw new InvalidOperationException("Kamera açılamadı. Kameranın bağlı olduğundan ve başka bir uygulama tarafından kullanılmadığından emin olun.");
            }
        }

        // yeni kare okuyamazsa null döner
        public Mat KareOku()
        {
            if (!Acik) return null;

            Mat kare = new Mat();
            if (!yakalayici.Read(kare) || kare.Empty())
            {
                kare.Dispose();
                return null;
            }
            Cv2.Flip(kare, kare, FlipMode.Y); // ayna görüntüsü daha doğal duruyor
            return kare;
        }

        public void Kapat()
        {
            if (yakalayici != null)
            {
                yakalayici.Release();
                yakalayici.Dispose();
                yakalayici = null;
            }
        }

        public void Dispose()
        {
            Kapat();
        }
    }
}
