using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mvvm_test1.Modbus_Rtu;
using Mvvm_test1.User_Page;
using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace Mvvm_test1.ViewModel
{
    class MainVM:ViewModelBase
    {
        private Main main;
      
        private System.Windows.Media.Brush brush_0;
        public System.Windows.Media.Brush Brush_0
        {
            get
            {
                return brush_0;
            }
            set
            {
                brush_0 = value;
                RaisePropertyChanged("Brush_0");
            }
        }
        DispatcherTimer timer;
        public MainVM(Main main)
        {
            this.main = main;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Modbus_Rtu_.modbus_rtu.CommunicationStatus)
                Brush_0 = Brushes.Red;
            else
                Brush_0 = Brushes.Gray;
        }
        public RelayCommand cclosedcommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    timer.Stop();
                });
            }
        }
        #region 首页加载
        public RelayCommand loadcommand
        {
            get
            {
                return new RelayCommand(loadcommandExecute);
            }
        }

        private void loadcommandExecute()
        {
            main.Background =new SolidColorBrush(Color.FromRgb(100, 201, 178));
            main.grd_user.Children.Add(new FristPage());
            
        }
        #endregion

        #region 首页按钮命令
        public RelayCommand btn_1
        {
            get
            {
                return new RelayCommand(btn_1Execute);
            }
        }

        private void btn_1Execute()
        {
            main.grd_user.Children.Clear();
            main.Background = new SolidColorBrush(Color.FromRgb(100, 201, 178));
            main.grd_user.Children.Add(new FristPage());
           
        }
        #endregion
        #region 第二页按钮命令
        public RelayCommand btn_2
        {
            get
            {
                return new RelayCommand(btn_2Execute);
            }
        }

        private void btn_2Execute()
        {
            main.grd_user.Children.Clear();
            main.Background = new SolidColorBrush(Color.FromRgb(234, 234, 30));
            main.grd_user.Children.Add(new SecondPage());
           
        }
        #endregion
    }
}
