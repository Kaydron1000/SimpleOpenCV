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

namespace SimpleOpenCV.Views
{
    /// <summary>
    /// Interaction logic for ThresholdProperties.xaml
    /// </summary>
    public partial class ThresholdPropertiesView : UserControl
    {
        public ThresholdPropertiesView()
        {
            InitializeComponent();

            foreach (var threshold in Enum.GetNames(typeof(ThresholdTypes)))
                Combobox_ThresholdTypes.Items.Add(threshold);
            Combobox_ThresholdTypes.SelectedIndex = 0;
        }
    }
}
