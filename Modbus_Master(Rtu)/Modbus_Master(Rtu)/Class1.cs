using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Modbus_Master_Rtu
{
    public class ModbusMasterRtu
    {
        static SerialPort port { get; set; }
       
        public static void NewPort(string portName, string baudRate, int parity, string dataBits, int stopBits)
        {
            port = new SerialPort();//新建串口

            port.PortName = portName;//设置串口名
            port.BaudRate = Convert.ToInt32(baudRate);//设置波特率
            port.Parity = (Parity)parity;//设置奇偶校验
            port.DataBits = Convert.ToInt32(dataBits);//设置数据位
            port.StopBits = (StopBits)stopBits;//设置停止位
            
        }
        public static bool OpenPort()//打开串口
        {
            try
            {
                if (port.IsOpen == true)
                    System.Windows.Forms.MessageBox.Show("串口已经打开");
                else
                    port.Open();
            }
            catch(Exception error)
            {
                
                System.Windows.Forms.MessageBox.Show(error.ToString());
            }
            return port.IsOpen;
        }
        public static bool ClosePort()
        {
            try
            {
                port.Close();
            }
            catch(Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.ToString());
            }
            return port.IsOpen;
        }
    }
}
