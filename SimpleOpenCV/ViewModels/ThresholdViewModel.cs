using SimpleOpenCV;
using SimpleOpenCV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOpenCV.ViewModels
{
    internal class ThresholdViewModel : INotifyPropertyChanged
    {
        private ImageTranslation_Threshold _imageTranslationScale;

        public ImageTranslation_Threshold ImageTranslationThreshold
        {
            get => _imageTranslationScale;
            set
            {
                _imageTranslationScale = value;
                OnPropertyChanged(nameof(ImageTranslationThreshold));
            }
        }

        public ThresholdViewModel()
        {
            // Initialize your ImageTranslation_Scale instance
            ImageTranslationThreshold = new ImageTranslation_Threshold();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
