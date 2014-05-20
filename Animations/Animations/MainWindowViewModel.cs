using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Controls;
using System.Reflection;

namespace Animations
{
    
    public class MainWindowViewModel : BaseViewModel
    {
        #region SpinCommand
        public ICommand SpinCommand { get; private set; }
        bool isSpinning = false;
        private void SpinExecute(object parameter)
        {
            MainWindow mainWindow = parameter as MainWindow;
            if (!isSpinning && mainWindow != null)
            {
                isSpinning = true;

                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = 0;
                doubleAnimation.To = 360;
                doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(4));
                doubleAnimation.Completed += (o, a) => { isSpinning = false; };

                mainWindow.btnSpin.RenderTransform = new RotateTransform();

                mainWindow.btnSpin.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
            }
        }
        #endregion

        #region FadeCommand
        public ICommand FadeCommand { get; private set; }
        private void FadeExecute(object parameter)
        {
            Button btnSpin = parameter as Button;
            if (btnSpin != null)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = 1.0;
                doubleAnimation.To = 0.0;

                btnSpin.BeginAnimation(Button.OpacityProperty, doubleAnimation);
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            SpinCommand = new DelegateCommand(null, SpinExecute);
            FadeCommand = new DelegateCommand(null, FadeExecute);

            //RGBControlViewModel = new RGBControlViewModel();

            AllColors = typeof(Colors).GetProperties().ToList<PropertyInfo>().Select(pi => pi.Name);
            SelectedColor = "DarkSeaGreen";
        }

        //public RGBControlViewModel RGBControlViewModel { get; private set; }

        public IEnumerable<string> AllColors { get; set; }

        private object selectedColor;
        public object SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    SelectedColorBrush = (Color)ColorConverter.ConvertFromString(value.ToString());
                    RaisePropertyChanged("SelectedColor");
                }
            }
        }

        private Color selectedColorBrush;
        public Color SelectedColorBrush
        {
            get { return selectedColorBrush; }
            set
            {
                if (selectedColorBrush != value)
                {
                    selectedColorBrush = value;
                    RaisePropertyChanged("SelectedColorBrush");
                }
            }
        }
        
    }
}
