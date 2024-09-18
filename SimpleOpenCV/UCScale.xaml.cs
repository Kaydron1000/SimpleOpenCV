using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for UCScale.xaml
    /// </summary>
    public partial class UCScale : UserControl
    {

        public UCScale()
        {
            InitializeComponent();
            foreach (var itm in Enum.GetNames(typeof(InterpolationFlags)))
            {
                comboBox_InterpolationFlags.Items.Add(itm);
            }
        }
    }
}
