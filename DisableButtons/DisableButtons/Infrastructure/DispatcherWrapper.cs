using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace DisableButtons.Infrastructure
{
    interface IDispatcherWrapper
    {
        Dispatcher Dispatcher { get; }
    }

    class DispatcherWrapper : IDispatcherWrapper
    {
        public Dispatcher Dispatcher
        {
            get { return App.Current.Dispatcher; }
        }
    }
}
