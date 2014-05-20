using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlLesson
{
    /// <summary>
    /// Interaction logic for RGBControl.xaml
    /// </summary>
    public partial class RGBControl : UserControl
    {
        public RGBControl() : this(255, 0, 0)
        {
        }

        public RGBControl(byte red, byte green, byte blue)
        {
            InitializeComponent();

            txtRed.Text = red.ToString();
            txtGreen.Text = green.ToString();
            txtBlue.Text = blue.ToString();
        }

        private void txtRGB_Changed(object sender, TextChangedEventArgs e)
        {
            byte red;
            byte.TryParse(txtRed.Text, out red);

            byte green;
            byte.TryParse(txtGreen.Text, out green);

            byte blue;
            byte.TryParse(txtBlue.Text, out blue);

            Color = Color.FromRgb(red, green, blue);
        }

        public Color Color
        {
            get { return (Color)base.GetValue(ColorProperty); }
            set { base.SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color",
            typeof(Color),
            typeof(RGBControl),
            new UIPropertyMetadata(Color.FromRgb(0, 0, 255), new PropertyChangedCallback(ColorPropertyChanged)));

        private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RGBControl rgbControl = (RGBControl)d;

            Color color = (Color)e.NewValue;
            rgbControl.colorArea.Fill = new SolidColorBrush(color);
            rgbControl.txtRed.Text = color.R.ToString();
            rgbControl.txtGreen.Text = color.G.ToString();
            rgbControl.txtBlue.Text = color.B.ToString();
        }

        
        
    }
}
