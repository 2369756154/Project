using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mvvm_test1.User_Page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Mvvm_test1.ViewModel
{
    class FristPageVM: DependencyObject
    {
        private FristPage fristPage;
        #region 依赖项属性注册,设置
      
        public ObservableCollection<string> MyPLCDatas
        {
            get { return (ObservableCollection<string>)GetValue(PLCDatasProperty); }
            set { SetValue(PLCDatasProperty, value);
            }
        }
       //添加依赖项属性，后台数据更改的时候通知UI更新数据
        private static readonly DependencyProperty PLCDatasProperty=
            DependencyProperty.Register("MyPLCDatas", typeof(ObservableCollection<string>), typeof(FristPageVM));

        #endregion
        public FristPageVM(FristPage fristPage)
        { 
            this.fristPage = fristPage;
            BindingOperations.SetBinding(this, PLCDatasProperty, new Binding("PLCdatas") { Source = Modbus_Rtu.Modbus_Rtu_.modbus_rtu});//数据绑定
        }
        public RelayCommand PageLoaded//页面加载命令
        {
            get
            {
                return new RelayCommand(() =>{ Modbus_Rtu.Modbus_Rtu_.modbus_rtu.ReadWords(1, 0, 10); } );
            }
        }
        public RelayCommand PageClosed//页面关闭命令
        {
            get
            {
                return new RelayCommand(
                 () => {
                     int l = Modbus_Rtu.Modbus_Rtu_.modbus_rtu.readData.Count;
                     if (l >0)
                         Modbus_Rtu.Modbus_Rtu_.modbus_rtu.readData.RemoveAt(l - 1);

                 }
                   );
            }
        }
    }
}
