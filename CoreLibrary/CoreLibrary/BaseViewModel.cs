using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CoreLibrary
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //usage: () => PropABC
        //e.g., RaisePropertyChanged<string>(() => Text1);
        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("memberExpression");
            }
            MemberExpression body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("The body must be a member expression");
            }
            RaisePropertyChanged(body.Member.Name);
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
