using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary;
using System.Windows.Input;
using Multithreading.Business;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Multithreading.Presentation
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand ButtonCommand { get; private set; }
        public void ExecuteButtonCommand(object parameter)
        {
            ThreadingModel threadingModel = new ThreadingModel();
            threadingModel.ThreadStatusChanged += OnThreadStatusChanged;
            threadingModel.Start();
            //threadingModel.StartWithExtensionMethodPost();

            //LongCalculation();
        }

        void OnThreadStatusChanged(ThreadingModel caller, ThreadStatusEventArgs args)
        {
            StatusList.Add(args.Value.ToString());
        }

        #region properties
        private ObservableCollection<string> statusList;
        public ObservableCollection<string> StatusList
        {
            get { return statusList; }
            private set
            {
                if (statusList != value)
                {
                    statusList = value;
                    RaisePropertyChanged(() => this.StatusList);
                }
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            private set
            {
                if (status != value)
                {
                    status = value;
                    RaisePropertyChanged(() => this.Status);
                }
            }
        }
        #endregion

        #region ctor
        public MainWindowViewModel ()
	    {
            ButtonCommand = new DelegateCommand(ExecuteButtonCommand);
            StatusList = new ObservableCollection<string>();
        }
        #endregion


        private void LongCalculation()
        {
            var watch = Stopwatch.StartNew();

            List<Task> tasks = new List<Task>();

            for (int i = 2; i < 10; i++)
            {
                int j = i;
                var t = Task.Factory.StartNew(() =>
                {
                    var result = SumRootN(j);
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        StatusList.Add(string.Format("root {0} : {1} ", j, result))
                    ), null);
                });
                tasks.Add(t);
            }

            Task.Factory.ContinueWhenAll(tasks.ToArray(), (r) =>
                {
                    var time = watch.ElapsedMilliseconds;
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        { Status = time.ToString(); }
                    ));
                });
            Status = watch.ElapsedMilliseconds.ToString();
        }

        public static double SumRootN(int root)
        {
            double result = 0;
            for (int i = 1; i < 10000000; i++)
            {
                result += Math.Exp(Math.Log(i) / root);
            }
            return result;
        }
    }
}
