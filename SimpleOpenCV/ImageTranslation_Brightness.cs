using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOpenCV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using OpenCvSharp;

    public class ImageTranslations_Brightness : INotifyPropertyChanged
    {
        private double _brightness;
        private double _gamma;
        private double _contrast;

        public double Brightness
        {
            get => _brightness;
            set => SetField(ref _brightness, value);
        }

        public double Gamma
        {
            get => _gamma;
            set => SetField(ref _gamma, value);
        }

        public double Contrast
        {
            get => _contrast;
            set => SetField(ref _contrast, value);
        }

        public ImageTranslations_Brightness()
        {
            // Set default values
            Brightness = 0;
            Gamma = 1.0;
            Contrast = 1.0;
        }

        public Mat ApplyAdjustments(Mat inputImage)
        {
            Mat adjustedImage = new Mat();
            inputImage.ConvertTo(adjustedImage, -1, Contrast, Brightness);
            Cv2.LUT(adjustedImage, GenerateGammaLookup(Gamma), adjustedImage);
            return adjustedImage;
        }

        private Mat GenerateGammaLookup(double gamma)
        {
            byte[] lookupTable = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                lookupTable[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / gamma)) + 0.5));
            }
            return null;// new Mat(1, 256, MatType.CV_8UC1, lookupTable);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
