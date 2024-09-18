using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOpenCV
{
    public class BlobDectectionParameters : INotifyPropertyChanged
    {
        private MainWindow _ParentView;
        private SimpleBlobDetector.Params _params;
        private SimpleBlobDetector _Detector;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _FilterByArea = false;
        private bool _FilterByCircularity = false;
        private bool _FilterByColor = false;
        private bool _FilterByConvexity = false;
        private bool _FilterByInertia = false;

        private byte _BlobColor = 0;

        private float _MaxArea = 0.3F;
        private float _MinArea = 0.1F;

        private float _MaxCircularity = 0.3F;
        private float _MinCircularity = 0.1F;

        private float _MaxConvexity = 0.3F;
        private float _MinConvexity = 0.1F;

        private float _MaxInertiaRatio = 0.3F;
        private float _MinInertiaRatio = 0.1F;

        private float _MaxThreshold = 0.3F;
        private float _MinThreshold = 0.1F;

        private float _ThresholdStep = 4;

        private float _MinDistBetweenBlobs = 1.4F;
        private uint _MinRepeatability = 2;

        public bool FilterByArea
        {
            get => _FilterByArea;
            set => SetField(ref _FilterByArea, value);
        }

        public bool FilterByCircularity
        {
            get => _FilterByCircularity;
            set => SetField(ref _FilterByCircularity, value);
        }
        public bool FilterByColor
        {
            get => _FilterByColor;
            set => SetField(ref _FilterByColor, value);
        }
        public bool FilterByConvexity
        {
            get => _FilterByConvexity;
            set => SetField(ref _FilterByConvexity, value);
        }
        public bool FilterByInertia
        {
            get => _FilterByInertia;
            set => SetField(ref _FilterByInertia, value);
        }

        public byte BlobColor
        {
            get => _BlobColor;
            set => SetField(ref _BlobColor, value);
        }
        public float MaxArea
        {
            get => _MaxArea;
            set => SetField(ref _MaxArea, value);
        }
        public float MinArea
        {
            get => _MinArea;
            set => SetField(ref _MinArea, value);
        }

        public float MaxCircularity
        {
            get => _MaxCircularity;
            set => SetField(ref _MaxCircularity, value);
        }
        public float MinCircularity
        {
            get => _MinCircularity;
            set => SetField(ref _MinCircularity, value);
        }

        public float MaxConvexity
        {
            get => _MaxConvexity;
            set => SetField(ref _MaxConvexity, value);
        }
        public float MinConvexity
        {
            get => _MinConvexity;
            set => SetField(ref _MinConvexity, value);
        }

        public float MaxInertiaRatio
        {
            get => _MaxInertiaRatio;
            set => SetField(ref _MaxInertiaRatio, value);
        }
        public float MinInertiaRatio
        {
            get => _MinInertiaRatio;
            set => SetField(ref _MinInertiaRatio, value);
        }

        public float MaxThreshold
        {
            get => _MaxThreshold;
            set => SetField(ref _MaxThreshold, value);
        }
        public float MinThreshold
        {
            get => _MinThreshold;
            set => SetField(ref _MinThreshold, value);
        }

        public float ThresholdStep
        {
            get => _ThresholdStep;
            set => SetField(ref _ThresholdStep, value);
        }

        public float MinDistBetweenBlobs
        {
            get => _MinDistBetweenBlobs;
            set => SetField(ref _MinDistBetweenBlobs, value);
        }
        public uint MinRepeatability
        {
            get => _MinRepeatability;
            set => SetField(ref _MinRepeatability, value);
        }


        public BlobDectectionParameters()
        {
            _params = new SimpleBlobDetector.Params();
            InitParams();
            PropertyChanged += BlobDectectionParameters_PropertyChanged;
        }
        public BlobDectectionParameters(MainWindow ParentView)
        {
            _ParentView = ParentView;
            _params = new SimpleBlobDetector.Params();
            InitParams();
            PropertyChanged += BlobDectectionParameters_PropertyChanged;
        }
        private void InitParams()
        {
            _params.FilterByArea = this.FilterByArea;
            _params.FilterByCircularity = this.FilterByCircularity;
            _params.FilterByColor = this.FilterByColor;
            _params.FilterByConvexity = this.FilterByConvexity;
            _params.FilterByInertia = this.FilterByInertia;

            _params.BlobColor = this.BlobColor;

            _params.MaxArea = this.MaxArea;
            _params.MinArea = this.MinArea;

            _params.MaxCircularity = this.MaxCircularity;
            _params.MinCircularity = this.MinCircularity;

            _params.MaxConvexity = this.MaxConvexity;
            _params.MinConvexity = this.MinConvexity;

            _params.MaxInertiaRatio = this.MaxInertiaRatio;
            _params.MinInertiaRatio = this.MinInertiaRatio;

            _params.MaxThreshold = this.MaxThreshold;
            _params.MinThreshold = this.MinThreshold;

            _params.ThresholdStep = this.ThresholdStep;
            _params.MinDistBetweenBlobs = this.MinDistBetweenBlobs;

            _params.MinRepeatability = this.MinRepeatability;
        }

        private void BlobDectectionParameters_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Mat SourceMat = _ParentView.ResultImg;
            if (SourceMat != null)
            {
                if (sender is BlobDectectionParameters)
                {
                    if (e.PropertyName == nameof(FilterByArea))
                        _params.FilterByArea = this.FilterByArea;
                    if (e.PropertyName == nameof(FilterByCircularity))
                        _params.FilterByCircularity = this.FilterByCircularity;
                    if (e.PropertyName == nameof(FilterByColor))
                        _params.FilterByColor = this.FilterByColor;
                    if (e.PropertyName == nameof(FilterByConvexity))
                        _params.FilterByConvexity = this.FilterByConvexity;
                    if (e.PropertyName == nameof(FilterByInertia))
                        _params.FilterByInertia = this.FilterByInertia;

                    if (e.PropertyName == nameof(BlobColor))
                        _params.BlobColor = this.BlobColor;

                    if (e.PropertyName == nameof(MaxArea))
                        _params.MaxArea = this.MaxArea;
                    if (e.PropertyName == nameof(MinArea))
                        _params.MinArea = this.MinArea;

                    if (e.PropertyName == nameof(MaxCircularity))
                        _params.MaxCircularity = this.MaxCircularity;
                    if (e.PropertyName == nameof(MinCircularity))
                        _params.MinCircularity = this.MinCircularity;

                    if (e.PropertyName == nameof(MaxConvexity))
                        _params.MaxConvexity = this.MaxConvexity;
                    if (e.PropertyName == nameof(MinConvexity))
                        _params.MinConvexity = this.MinConvexity;

                    if (e.PropertyName == nameof(MaxInertiaRatio))
                        _params.MaxInertiaRatio = this.MaxInertiaRatio;
                    if (e.PropertyName == nameof(MinInertiaRatio))
                        _params.MinInertiaRatio = this.MinInertiaRatio;

                    if (e.PropertyName == nameof(MaxThreshold))
                        _params.MaxThreshold = this.MaxThreshold;
                    if (e.PropertyName == nameof(MinThreshold))
                        _params.MinThreshold = this.MinThreshold;

                    if (e.PropertyName == nameof(ThresholdStep))
                        _params.ThresholdStep = this.ThresholdStep;
                    if (e.PropertyName == nameof(MinDistBetweenBlobs))
                        _params.MinDistBetweenBlobs = this.MinDistBetweenBlobs;

                    if (e.PropertyName == nameof(MinRepeatability))
                        _params.MinRepeatability = this.MinRepeatability;


                    _Detector = SimpleBlobDetector.Create(_params);

                    var kep = _Detector.Detect(SourceMat);

                    Cv2.DrawKeypoints(image: SourceMat, kep, _ParentView.ResultImg, Scalar.Beige, DrawMatchesFlags.DrawRichKeypoints);

                    _ParentView.UpdatedImg.Source = MainWindow.MatToBitmapImage(_ParentView.ResultImg);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
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
