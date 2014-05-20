using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multithreading.Business
{
    public class ThreadStatusEventArgs : EventArgs
    {
        public object Value { get; private set; }

        public ThreadStatusEventArgs(object value)
        {
            Value = value;
        }
    }
}
