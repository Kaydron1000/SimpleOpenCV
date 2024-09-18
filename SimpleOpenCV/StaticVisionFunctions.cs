using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace SimpleOpenCV
{
    public static class StaticVisionFunctions
    {
        public static Mat Erode (this Mat inputImage, int kernelWidth, int kernelHeight, MorphShapes shape, out Dictionary<string,object> variables)
        {
            // Define the structuring element (kernel) for erosion
            Mat kernel = Cv2.GetStructuringElement(shape, new Size(kernelWidth, kernelHeight));

            // Perform erosion
            Mat erodedImage = new Mat();
            Cv2.Erode(inputImage, erodedImage, kernel);

            variables = new Dictionary<string, object>();
            variables.Add("STEP01-IN_KernelWidth", kernelWidth);
            variables.Add("STEP01-IN_KernelHeight", kernelHeight);
            variables.Add("STEP01-IN_shape", shape);

            return erodedImage;
        }
        public static Mat Resize(this Mat src, Size newSize)
        {
            Mat dst = new Mat();
            Cv2.Resize(src, dst, newSize);
            
             
            return dst;
        }
        public static Mat ComputeHistogram(this Mat src)
        {
            Mat histogram = new Mat();
            int[] channels = { 0 }; // Compute histogram for the first channel only
            int[] histSize = { 256 }; // Number of bins
            Rangef[] ranges = { new Rangef(0, 256) }; // Range of pixel values

            Cv2.CalcHist(new Mat[] { src }, channels, null, histogram, 1, histSize, ranges);

            return histogram;
        }
    }
}
