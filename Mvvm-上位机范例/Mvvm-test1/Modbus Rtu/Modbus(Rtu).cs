using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Threading;

namespace Mvvm_test1.Modbus_Rtu
//单例模式
{
    class Modbus_Rtu_: IDisposable
    {
        private static object obj = new object();
        public int _start { get; set; }   
        private static Modbus_Rtu_ _modbus_rtu;//内部静态对象
        public static Modbus_Rtu_ modbus_rtu//外部访问的静态对象
        {
            get {
                if (_modbus_rtu == null)
                    _modbus_rtu = new Modbus_Rtu_();
                return _modbus_rtu;
            }
        }
        const byte READ_WORDS = 0X03;//读多个寄存器
        const byte READ_BITS = 0X01;//读多个位
        const byte WRITE_BIT = 0X05;//写单个位
        const byte WRITE_WORD = 0X06;//写单个寄存器
        const byte WRITE_BITS = 0X0F;//写多个位
        const byte WRITE_WORDS = 0X10;//写多个寄存器
        public  bool CommunicationStatus { get; set; }//通讯状态
        private  SerialPort port1;
        private DispatcherTimer timer;
        public  List<byte[]> writeData = new List<byte[]>();//写入命令队列
        public  List<byte[]> readData = new List<byte[]>();//读取命令队列
        private List<byte> buffer = new List<byte>();
        public ObservableCollection<string> PLCdatas{ get; set; }//存储读取的数据
        public byte[] PLCbits {get;set;}//存储PLC的位状态
        private int start = 0;//数据的起始地址
        private int rdi = 0;//临时变量
        byte PlcAddress;//PLC站号
        private Modbus_Rtu_()
        {
            port1 = new SerialPort();//实例化串口类
            timer = new DispatcherTimer();//实例化定时器
            port1.DataReceived += Port1_DataReceived;//注册串口事件
            timer.Interval = TimeSpan.FromMilliseconds(60);//设置定时器间隔时间
            timer.Tick += Timer_Tick;//注册定时器事件
            PLCdatas = new ObservableCollection<string>(new string[30000]);//实例化一个数组用来接收PLC数据
            PLCbits = new byte[30000];     //实例化数组用来存放数据的位地址 
        }   
        public  void ReadWords(int _plcaddr, int start, int num)//发送读取多个寄存器命令
        {
            List<byte> _bytes = new List<byte>();
            _bytes.Add((byte)(_plcaddr));//站号
            _bytes.Add(READ_WORDS);//功能码
            _bytes.Add((byte)(start >> 8));//起始地址高位
            _bytes.Add((byte)(start));//起始地址低位
            _bytes.Add((byte)(num >> 8));//字节数高位
            _bytes.Add((byte)(num));//字节数低位
            _bytes.AddRange(crc_16(_bytes.ToArray()));
            readData.Add( _bytes.ToArray());
            
        }
        public  void ReadBits(int _plcaddr, int start, int num)//发送读取多个线圈命令
        {
            List<byte> _bytes = new List<byte>();
            _bytes.Add((byte)(_plcaddr));//站号
            _bytes.Add(READ_BITS);//功能码
            _bytes.Add((byte)(start >> 8));//起始地址高位
            _bytes.Add((byte)(start));//起始地址低位
            _bytes.Add((byte)(num >> 8));//字节数高位
            _bytes.Add((byte)(num));//字节数低位
            _bytes.AddRange(crc_16(_bytes.ToArray()));
            readData.Add(_bytes.ToArray());
        }
        public  void WriteWord(int _plcaddr, int start, string text)
        {
            List<byte> _bytes = new List<byte>();
            _bytes.Add((byte)(_plcaddr));//站号
            _bytes.Add(WRITE_WORD);//功能码
            _bytes.Add((byte)(start >> 8));//起始地址高位
            _bytes.Add((byte)(start));//起始地址低位
            int num = Convert.ToInt32(text);
            _bytes.Add((byte)(num >> 8));//字节数高位
            _bytes.Add((byte)(num));//字节数低位
            _bytes.AddRange(crc_16(_bytes.ToArray()));
             writeData.Add(_bytes.ToArray());
        }
        public  byte[] crc_16(byte[] data)//CRC计算方法
        {
            uint IX, IY;
            ushort crc = 0xFFFF;//set all 1

            int len = data.Length;
            if (len <= 0)
                crc = 0;
            else
            {
                len--;
                for (IX = 0; IX <= len; IX++)
                {
                    crc = (ushort)(crc ^ (data[IX]));
                    for (IY = 0; IY <= 7; IY++)
                    {
                        if ((crc & 1) != 0)
                            crc = (ushort)((crc >> 1) ^ 0xA001);
                        else
                            crc = (ushort)(crc >> 1); //
                    }
                }
            }
            byte[] modbus_crc = new byte[2];
            modbus_crc[1] = (byte)((crc & 0xff00) >> 8);//高位置
            modbus_crc[0] = (byte)(crc & 0x00ff); //低位置
            return modbus_crc;
        }
        public  bool Crcjiaoyan(List<byte> t)
        {


            List<byte> _t = t.ToList<byte>();
            _t.RemoveAt(_t.Count - 1);
            _t.RemoveAt(_t.Count - 1);
            byte[] l = _t.ToArray();//赋值变量到临时区存放
            byte[] crc = new byte[2];
            crc = crc_16(l);
            if (crc[0] == t[t.Count - 2] && crc[1] == t[t.Count - 1])
                return true;
            else
                return false;
        }// CRC校验码对比
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            { 
            lock (obj)
            {
                if (port1.IsOpen)
                {
                    CommunicationStatus = false;
                    if (writeData.Count > 0)
                    {
                        PlcAddress = writeData[0][0];
                        port1.Write(writeData[0].ToArray(), 0, writeData[0].Length);
                        writeData.RemoveAt(0);
                    }
                    else
                    {
                        if (readData.Count > 0)
                        {
                            if (rdi < readData.Count)
                            {
                                byte _l = readData[rdi][0];
                                PlcAddress = _l;
                                start = (int)(readData[rdi][2] << 8) + readData[rdi][3];
                                port1.Write(readData[rdi].ToArray(), 0, readData[rdi].Length);

                                rdi++;
                                if (rdi >= readData.Count)
                                    rdi = 0;
                            }
                        }
                    }

                }
            }
        }
            catch
            {
                System.Windows.Forms.MessageBox.Show("串口异常，请检查串口线，然后重新启动程序");
                return;
            }

        }//定时器启动定时发送读取命令
        private void Port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            CommunicationStatus = true;
            try
            {
                System.Threading.Thread.Sleep(8);
                int num = port1.BytesToRead;
                byte[] buf = new byte[num];
                port1.Read(buf, 0, num);
                buffer.AddRange(buf);
                if (buffer.Count > 5)
                {
                    if (buffer[0] == PlcAddress)
                    {
                        if (buffer[1] == 0x03)
                        {
                            int len = buffer[2];
                            if (buffer.Count < len + 5)
                            {
                                return;
                            }
                            else
                            {
                                if (Crcjiaoyan(buffer))
                                {

                                    for (int i = 0; i < len; i = i + 2)
                                    {
                                        PLCdatas[start + i / 2] = ((int)(buffer[i + 3] << 8) + buffer[i + 4]).ToString();

                                    }
                                    buffer.RemoveRange(0, len + 5);
                                }
                                else
                                {
                                    port1.DiscardInBuffer();
                                    buffer.Clear();
                                    return;
                                }
                            }
                        }
                        else if (buffer[1] == 0x01)
                        {
                            int len = buffer[2];
                            if (buffer.Count < len + 5)
                            {
                                return;
                            }
                            else
                            {
                                if (Crcjiaoyan(buffer))
                                {

                                    for (int i = 0; i < len;i++)
                                    {
                                        for (int j = 0; j < 8; j++)
                                        {
                                            PLCbits[start + i * 8+j] =(byte) ((buffer[i + 3] >> j) % 2);
                                        }
                                                                                                                                              
                                    }
                                    buffer.RemoveRange(0, len + 5);
                                }
                                else
                                {
                                    port1.DiscardInBuffer();
                                    buffer.Clear();
                                    return;
                                }
                            }

                        }
                        else
                        {
                            port1.DiscardInBuffer();
                            buffer.Clear();
                            return;
                        }

                    }
                    else
                        buffer.RemoveAt(0);
                }

                else
                    return;

            }
            catch
            {
                return;
            }
            CommunicationStatus = false;
        }//串口接收事件
        public  void OpenPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {


          
            port1.PortName = portName;
            port1.BaudRate = baudRate;
            port1.Parity = parity;
            port1.DataBits = dataBits;
            port1.StopBits = stopBits;
            port1.Open();
          
           StartTimer();

        }//打开串口命令
        public  void ColsedPort()//关闭串口
        {
            try
            {
                port1.DiscardOutBuffer();
                port1.DiscardInBuffer();
                writeData.Clear();
                readData.Clear();  
                  timer.Stop();
                 port1.Close();
            }
            catch
            {
                return;
            }

        }
        public void ClearReadWriteWords()//清空读取命令队列
        {
            
        }
        private void StartTimer()//启动定时器，注册定时器，注册串口接收事件，让他们都属于同一个对象。
        {
            CommunicationStatus = false;

            timer.Start();
               
        }
        public void ClosePage()//注销定时器和解绑串口触发事件 让GC回收当前对象
        {
            timer.Stop();
            port1.DataReceived -= Port1_DataReceived;
        }
        public void Dispose()
        {
            timer.Stop();
            port1.Close();
        }
    }
}
