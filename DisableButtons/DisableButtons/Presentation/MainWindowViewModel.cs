using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary;
using System.Windows.Input;
using System.Threading;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DisableButtons.Infrastructure;
using System.Windows.Threading;

namespace DisableButtons.Presentation
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region FooCommand
        public ICommand FooCommand { get; private set; }
        public void ExecuteFooCommand(object parameter)
        {
            new Thread(() => 
            {
                IsFooEnabled = false;
                Thread.Sleep(2000); 
                IsFooEnabled = true; 
            }).Start();
        }

        private bool isFooEnabled;
        public bool IsFooEnabled 
        {
            private get { return isFooEnabled; }
            set
            {
                if (isFooEnabled != value)
                {
                    isFooEnabled = value;
                    RaisePropertyChanged<bool>(() => IsFooEnabled);
                }
            }
        }
        #endregion

        #region MooCommand
        private bool mooEnabled = true;
        public ICommand MooCommand { get; private set; }
        public void ExecuteMooCommand(object parameter)
        {
            new Thread(() =>
            {
                mooEnabled = false;
                App.Current.Dispatcher.Invoke(new Action(() => (MooCommand as DelegateCommand).RaiseCanExecuteChanged()), null);

                Thread.Sleep(2000);

                mooEnabled = true;
                App.Current.Dispatcher.Invoke(new Action(() => (MooCommand as DelegateCommand).RaiseCanExecuteChanged()), null);

            }).Start();
        }

        public bool CanExecuteMooCommand(object parameter)
        {
            return mooEnabled;
        }

        #endregion

        #region TooCommand
        public ICommand TooCommand { get; private set; }
        public void ExecuteTooCommand(object parameter)
        {
        }

        public bool CanExecuteTooCommand(object parameter)
        {
            return !(string.IsNullOrEmpty(Text1) ^ string.IsNullOrEmpty(Text2));
        }
        #endregion

        #region RandomCommand
        public ICommand RandomCommand { get; private set; }
        public void ExecuteRandomCommand(object parameter)
        {
            RandomList.Add("Test1");
            RandomList.Add("Test2");
            RandomList.Add("Test3");
            SynchronizationContext synchronizationContext = SynchronizationContext.Current;
            new Thread(() =>
            {
                App.Current.Dispatcher.Invoke(new Action(() => RandomList.Add("In Thread 4")));
                synchronizationContext.Send(o => RandomList.Add("In Thread 5"), null);
            }).Start();

            Task.Factory.StartNew(() => RandomList.Add("In Thread 6"),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext()
            ).
            ContinueWith((o) => RandomList.Add("In Thread 7"),
                TaskScheduler.FromCurrentSynchronizationContext()
            );

            var taskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
            taskFactory.StartNew(() => RandomList.Add("In Thread 8"));

            Dispatcher dispatcher = new DispatcherWrapper().Dispatcher;
            new Thread(() =>
            {
                dispatcher.Invoke(new Action(() => RandomList.Add("In Thread 9")));
            }).Start();
        }
        #endregion

        #region Text1 and Text2
        private string isText1;
        public string Text1
        {
            private get { return isText1; }
            set
            {
                if (isText1 != value)
                {
                    isText1 = value;
                    RaisePropertyChanged<string>(() => Text1);
                }
            }
        }

        private string isText2;
        public string Text2
        {
            private get { return isText2; }
            set
            {
                if (isText2 != value)
                {
                    isText2 = value;
                    RaisePropertyChanged<string>(() => Text2);
                }
            }
        }

        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == "Text1" || e.PropertyName == "Text2")
            {
                (TooCommand as DelegateCommand).RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region RandomList
        public ObservableCollection<String> RandomList { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            FooCommand = new DelegateCommand(ExecuteFooCommand);
            IsFooEnabled = true;

            MooCommand = new DelegateCommand(CanExecuteMooCommand, ExecuteMooCommand);

            TooCommand = new DelegateCommand(CanExecuteTooCommand, ExecuteTooCommand);

            RandomList = new ObservableCollection<string>();
            RandomCommand = new DelegateCommand(ExecuteRandomCommand);
        }
    }
}
