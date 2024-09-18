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

    public class ImageTranslation_Scale : INotifyPropertyChanged
    {
        private double _scaleFactor;
        private InterpolationFlags _interpolationFlags;

        public double ScaleFactor
        {
            get => _scaleFactor;
            set => SetField(ref _scaleFactor, value);
        }

        public InterpolationFlags InterpolationFlags
        {
            get => _interpolationFlags;
            set => SetField(ref _interpolationFlags, value);
        }

        public ImageTranslation_Scale()
        {
            // Set default values for scale factor and interpolation flags
            ScaleFactor = 1.0;
            InterpolationFlags = InterpolationFlags.Linear;
        }
        public ImageTranslation_Scale(Mat LoadImage)
        {
            // Set default values for scale factor and interpolation flags
            ScaleFactor = 1.0;
            InterpolationFlags = InterpolationFlags.Linear;
        }

        public Mat ApplyScaling(Mat inputImage)
        {
            // Calculate new width and height based on scale factor
            int newWidth = (int)(inputImage.Width * ScaleFactor);
            int newHeight = (int)(inputImage.Height * ScaleFactor);

            // Create a new Mat object to store the scaled image
            Mat scaledImage = new Mat();

            // Resize the input image to the new width and height using specified interpolation flags
            Cv2.Resize(inputImage, scaledImage, new Size(newWidth, newHeight), interpolation: InterpolationFlags);

            return scaledImage;
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
