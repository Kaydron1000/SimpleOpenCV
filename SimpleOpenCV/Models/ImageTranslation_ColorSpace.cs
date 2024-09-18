using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OpenCvSharp;

namespace SimpleOpenCV.Models
{
    public class ImageTranslation_ColorSpace : INotifyPropertyChanged
    {
        private ColorConversionCodes _colorSpaceCode;

        public ColorConversionCodes ColorSpaceCode
        {
            get => _colorSpaceCode;
            set => SetField(ref _colorSpaceCode, value);
        }

        public ImageTranslation_ColorSpace()
        {
            // Default color space conversion code
            ColorSpaceCode = ColorConversionCodes.BGR2GRAY;
        }

        public Mat ApplyColorSpaceConversion(Mat src)
        {
            Mat convertedImage = new Mat();
            Cv2.CvtColor(src, convertedImage, ColorSpaceCode);
            return convertedImage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
