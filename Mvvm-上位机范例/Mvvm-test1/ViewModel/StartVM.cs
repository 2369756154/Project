using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.IO.Ports;
using GalaSoft.MvvmLight.Command;

namespace Mvvm_test1.ViewModel
{
    class StartVM:ViewModelBase
    {
        private Start start;
      
        public StartVM(Start start)
        {
            //comm = new COMM();
            this.start = start;
        }

        public string portName { get; set; }
        public string baudRate { get; set; }
        public Parity parity { get; set; }
        public string dataBits { get; set; }
        public StopBits stopBits { get; set; }

        #region 确定按钮命令，打开串口，和主窗口
        public RelayCommand btn_openPort
        {
            get
            {
                return new RelayCommand(btn_openExecute);
            }
        }

        private void btn_openExecute()
        {
            try
            {

                Modbus_Rtu.Modbus_Rtu_.modbus_rtu.OpenPort(portName, Convert.ToInt32( baudRate), parity, Convert.ToInt16 (dataBits), stopBits);
                start.Hide();
                Main main = new Main(start);
                main.Show();
            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("串口异常，请检查");
                return;
            }
           
        }
        #endregion
    }
}
