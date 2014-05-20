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
using System.ComponentModel;
using CoreLibrary;
using System.Linq.Expressions;

namespace UserControlLesson
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            AllColors = typeof(Colors).GetProperties().ToList<PropertyInfo>().Select(pi => pi.Name);
            SelectedColor = AllColors.FirstOrDefault();
        }

        public IEnumerable<string> AllColors { get; set; }

        private string selectedColor;
        public string SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    RaisePropertyChanged(() => SelectedColor);
                }
            }
        }
        
    }
}
