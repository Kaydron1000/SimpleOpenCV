using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOpenCV.Models
{
    public class ImageTranslation_Threshold : INotifyPropertyChanged
    {
        private double _thresholdMinValue;
        private double _thresholdMaxValue;
        private ThresholdTypes _thresholdType;

        public double ThresholdMinValue
        {
            get => _thresholdMinValue;
            set => SetField(ref _thresholdMinValue, value);
        }

        public double ThresholdMaxValue
        {
            get => _thresholdMaxValue;
            set => SetField(ref _thresholdMaxValue, value);
        }

        public ThresholdTypes ThresholdType
        {
            get => _thresholdType;
            set => SetField(ref _thresholdType, value);
        }

        public ImageTranslation_Threshold()
        {
            // Default values
            ThresholdMinValue = 127;
            ThresholdMaxValue = 255;
            ThresholdType = ThresholdTypes.Binary;
        }

        public Mat ApplyThreshold(Mat src)
        {
            Mat thresholded = new Mat();
            Cv2.Threshold(src, thresholded, ThresholdMinValue, ThresholdMaxValue, ThresholdType);
            return thresholded;
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
