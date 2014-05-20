using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace MultithreadingLesson.Business
{
    public class Operations
    {
        public string VeryLongOperation(string parameter)
        {
            Thread.Sleep(5000);

            return parameter;
        }
    }
}
