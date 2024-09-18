using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleOpenCV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged
    {
        public System.Windows.Point _mouseClickPos;
        public bool bMoving;
        public bool bScale;


        private void viewboxMain_MouseMove(object sender, MouseEventArgs e)
        {

            if (bMoving)
            {
                //get current transform
                TranslateTransform transform = (viewboxMain.RenderTransform as TransformGroup).Children[0] as TranslateTransform;

                System.Windows.Point currentPos = e.GetPosition(viewboxBackground);
                transform.X += (currentPos.X - _mouseClickPos.X);
                transform.Y += (currentPos.Y - _mouseClickPos.Y);

                //viewboxMain.RenderTransform = transform;

                _mouseClickPos = currentPos;
            }
        }

        private void viewboxMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseClickPos = e.GetPosition(viewboxBackground);
            bMoving = true;
        }

        private void viewboxMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bMoving = false;
        }

        private void ScrollViewer_KeyUp(object sender, KeyEventArgs e)
        {
            bScale = false;
        }

        private void ScrollViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                bScale = true;
            }
        }
        private void viewboxMain_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void viewboxMain_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        private void viewboxMain_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (bScale)
            {
                ScaleTransform transform = (viewboxMain.RenderTransform as TransformGroup).Children[1] as ScaleTransform;
                if (e.Delta > 0)
                {
                    transform.ScaleX += 1 + e.Delta / 1000.0;
                    transform.ScaleY += 1 + e.Delta / 1000.0;
                }
                else if (e.Delta < 0)
                {
                    transform.ScaleX -= 1 + e.Delta / 1000.0;
                    transform.ScaleY -= 1 + e.Delta / 1000.0;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private BlobDectectionParameters _BlobParams;
        public BlobDectectionParameters BlobParams 
        {
            get => _BlobParams; 
            set => SetField(ref _BlobParams, value);
        }
        public Mat SourceImg;
        public Mat ResultImg;
        public MainWindow()
        {
            InitializeComponent();

            viewboxMain.RenderTransform = new TransformGroup();
            (viewboxMain.RenderTransform as TransformGroup).Children.Add(new TranslateTransform());
            (viewboxMain.RenderTransform as TransformGroup).Children.Add(new ScaleTransform());

            BlobParams = new BlobDectectionParameters(this);
            DataContext = this;
            string imgFilePath = @"E:\Code\CSharp\SimpleOpenCV\SimpleOpenCV\Assets\TestImg.png";
            SourceImg = Cv2.ImRead(imgFilePath,ImreadModes.Color);
            
            //BaseImg.Source = new BitmapImage(new Uri(imgFilePath, UriKind.Absolute));
            Mat binaryImg = new Mat(SourceImg.Size(), MatType.CV_8UC1);
            Cv2.CvtColor(SourceImg, binaryImg, ColorConversionCodes.BGRA2GRAY);
            
            ResultImg = new Mat();

            double aa = Cv2.Threshold(binaryImg, binaryImg, 127, 255, ThresholdTypes.Binary);
            ResultImg = binaryImg;
            UpdatedImg.Source = MatToBitmapImage(ResultImg);
            //SimpleBlobDetector.Params blobParams = new SimpleBlobDetector.Params();
            //blobParams.MinThreshold = 10f;
            //blobParams.MaxThreshold = 200f;
            //blobParams.FilterByArea = true;
            //blobParams.MinArea = 100f;
            //blobParams.FilterByCircularity = true;
            //blobParams.MinCircularity = 0.8f;
            //blobParams.FilterByConvexity = true;
            //blobParams.MinConvexity = 0.87f;
            //blobParams.FilterByInertia = true;
            //blobParams.MinInertiaRatio = 0.01f;

            //var detector = SimpleBlobDetector.Create(blobParams);

            //KeyPoint[] pts = detector.Detect(SourceImg);
            
            //Cv2.DrawKeypoints(SourceImg, pts, ResultImg, new Scalar(0, 0, 255), DrawMatchesFlags.Default);

            //UpdatedImg.Source = MatToBitmapImage(ResultImg);

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
        public static BitmapImage MatToBitmapImage(Mat mat)
        {
            try
            {
                // Convert Mat to .NET Bitmap

                Bitmap bitmap  = new Bitmap(mat.ToMemoryStream(".bmp"));
                // Convert Bitmap to BitmapImage
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Bmp);
                    stream.Seek(0, SeekOrigin.Begin);

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }

                return bitmapImage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
