using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using OpenCvSharp;
using SimpleOpenCV.Models;

namespace SimpleOpenCV.ViewModels
{
    public class ColorSpaceViewModel : INotifyPropertyChanged
    {
        private ImageTranslation_ColorSpace _imageTranslationColorSpace;
        private Collection<string> _availableColorSpaceCodes;
        private string _selectedColorSpace;
        private static Regex _regex = new Regex("(BGR_FULL)|(RGB_FULL)|(LBGR)|(NV12)|(NV21)|(YV12)|(IYUV)|(420)|(YVYU)|(UYVY)|(YUY2)|(mRGBA)|(I420)|(YV12)|(BayerBG)|(BayerGB)|(BayerRG)|(BayerGR)|(BGR_VNG)|(BGR_EA)");

        private Dictionary<string, string> _descs = new Dictionary<string, string>()
        {
            { "YUV", "Luminance,Blue Projection,Red Projection" },
            { "YCbCr", "Luminance,Blue-difference chroma,Red-difference chroma" },
            { "BGR", "Blue,Green,Red" },
            { "RGB", "Red,Green,Blue" },
            { "BGRA", "Blue,Green,Red,Alpha" },
            { "RGBA", "Red,Green,Blue,Alpha" },
            { "GRAY", "Gray" },
            { "BGR555", "5 bit Blue,5 bit Green,5 bit Red" },
            { "BGR565", "5 bit Blue,6 bit Green,5 bit Red" },
            { "XYZ", "CIE Red,CIE Green,CIE Blue" },
            { "HSV", "Hue,Saturation,Value(Brightness)" },
            { "Lab", "Luminance,Green to Red,Blue to Yellow" },
            { "Luv", "Luminance,Green to Magenta,Blue to Yellow" },
            { "HLS", "Hue,Luminance,Saturation" },
            { "HSV_FULL", "Hue,Saturation,Value(Brightness)" },
            { "HLS_FULL", "Hue,Luminance,Saturation" }
        };
        #region checkBoxes
        private bool? _isAllChannelsChecked;
        private Visibility _checkbox1Visible;
        private Visibility _checkbox2Visible;
        private Visibility _checkbox3Visible;
        private bool _isChannel1Checked;
        private bool _isChannel2Checked;
        private bool _isChannel3Checked;
        private bool _updatingCheckBoxes;
        private string _channel1CheckboxContent;
        private string _channel2CheckboxContent;
        private string _channel3CheckboxContent;

        public Visibility Checkbox1Visible
        {
            get => _checkbox1Visible;
            set => SetField(ref _checkbox1Visible, value);
        }
        public Visibility Checkbox2Visible
        {
            get => _checkbox2Visible;
            set => SetField(ref _checkbox2Visible, value);
        }
        public Visibility Checkbox3Visible
        {
            get => _checkbox3Visible;
            set => SetField(ref _checkbox3Visible, value);
        }
        public string Channel1CheckboxContent
        {
            get => _channel1CheckboxContent;
            set => SetField(ref _channel1CheckboxContent, value);
        }
        public string Channel2CheckboxContent
        {
            get => _channel2CheckboxContent;
            set => SetField(ref _channel2CheckboxContent, value);
        }
        public string Channel3CheckboxContent
        {
            get => _channel3CheckboxContent;
            set => SetField(ref _channel3CheckboxContent, value);
        }
        public bool? IsAllChannelsChecked
        {
            get => _isAllChannelsChecked;
            set => SetFieldCheckBoxes(ref _isAllChannelsChecked, value);
        }

        public bool IsChannel1Checked
        {
            get => _isChannel1Checked;
            set => SetFieldCheckBoxes(ref _isChannel1Checked, value);
        }

        public bool IsChannel2Checked
        {
            get => _isChannel2Checked;
            set => SetFieldCheckBoxes(ref _isChannel2Checked, value);
        }

        public bool IsChannel3Checked
        {
            get => _isChannel3Checked;
            set => SetFieldCheckBoxes(ref _isChannel3Checked, value);
        }
        private void SetFieldCheckBoxes<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!_updatingCheckBoxes)
            {

                if (value is bool val)
                {
                    _updatingCheckBoxes = true;
                    if (propertyName == nameof(IsAllChannelsChecked))
                    {
                        SetField(ref _isChannel1Checked, val);
                        SetField(ref _isChannel2Checked, val);
                        SetField(ref _isChannel3Checked, val);
                    }
                    else if (propertyName == nameof(IsChannel1Checked))
                    {
                        SetField(ref _isChannel1Checked, val);
                    }
                    else if (propertyName == nameof(IsChannel2Checked))
                    {
                        SetField(ref _isChannel2Checked, val);
                    }
                    else if (propertyName == nameof(IsChannel3Checked))
                    {
                        SetField(ref _isChannel3Checked, val);
                    }
                    
                    if (_isChannel1Checked && _isChannel2Checked && _isChannel3Checked)
                    {
                        SetField(ref _isAllChannelsChecked, true);
                    }
                    else if (!_isChannel1Checked && !_isChannel2Checked && !_isChannel3Checked)
                    {
                        SetField(ref _isAllChannelsChecked, false);
                    }
                    else
                    {
                        SetField(ref _isAllChannelsChecked, null);
                    }
                    _updatingCheckBoxes = false;
                }
            }
        }
        #endregion
        public Collection<string> AvailableColorSpaceCodes
        {
            get => _availableColorSpaceCodes;
            set => SetField(ref _availableColorSpaceCodes, value);
        }
        public string SelectedColorSpace
        {
            get => _selectedColorSpace;
            set
            {
                SetField(ref _selectedColorSpace, value);
                ImageTranslationColorSpace.ColorSpaceCode = (ColorConversionCodes)Enum.Parse(typeof(ColorConversionCodes), value);
                string descs = _descs.First(o => value.EndsWith(o.Key)).Value;
                string[] channelDescs = descs.Split(',');

                if (channelDescs.Length >= 1)
                {
                    Channel1CheckboxContent = channelDescs[0];
                    Checkbox1Visible = Visibility.Visible;
                } 
                else
                {
                    Checkbox1Visible = Visibility.Hidden;
                }
                if (channelDescs.Length >= 2)
                {
                    Channel2CheckboxContent = channelDescs[1];
                    Checkbox2Visible = Visibility.Visible;
                }
                else
                {
                    Checkbox2Visible = Visibility.Hidden;
                }
                if (channelDescs.Length >= 3)
                {
                    Channel3CheckboxContent = channelDescs[2];
                    Checkbox3Visible = Visibility.Visible;
                }
                else
                {
                    Checkbox3Visible = Visibility.Hidden;
                }
                IsAllChannelsChecked = true;
            }
        }
        public ColorSpaceViewModel()
        {
            _imageTranslationColorSpace = new ImageTranslation_ColorSpace();

            List<string> availableColorConversions = Enum.GetNames(typeof(ColorConversionCodes)).Where(o => !_regex.IsMatch(o)).ToList();
            _availableColorSpaceCodes = new Collection<string>(availableColorConversions);
        }


        public ImageTranslation_ColorSpace ImageTranslationColorSpace
        {
            get => _imageTranslationColorSpace;
            set => SetField(ref _imageTranslationColorSpace, value);
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
