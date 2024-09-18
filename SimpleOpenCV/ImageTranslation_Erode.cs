using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOpenCV
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using OpenCvSharp;

    public class ImageTranslation_Erode : INotifyPropertyChanged
    {
        private int _kernelWidth;
        private int _kernelHeight;
        private MorphShapes _shape;

        public int KernelWidth
        {
            get => _kernelWidth;
            set => SetField(ref _kernelWidth, value);
        }

        public int KernelHeight
        {
            get => _kernelHeight;
            set => SetField(ref _kernelHeight, value);
        }

        public MorphShapes Shape
        {
            get => _shape;
            set => SetField(ref _shape, value);
        }

        public ImageTranslation_Erode()
        {
            // Set default values
            KernelWidth = 3;
            KernelHeight = 3;
            Shape = MorphShapes.Rect;
        }

        public Mat ApplyErosion(Mat inputImage)
        {
            // Define the structuring element (kernel) for erosion
            Mat kernel = Cv2.GetStructuringElement(Shape, new Size(KernelWidth, KernelHeight));

            // Perform erosion
            Mat erodedImage = new Mat();
            Cv2.Erode(inputImage, erodedImage, kernel);

            return erodedImage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
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
