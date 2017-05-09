using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Web.Services;

namespace Mvvm_test1
{
    //自己定制数值输入器控件
    class NumTextBox:TextBox
    {
       public enum InputTypes//定义枚举提供数值输入器选择输入的数据
        {
            Uint32,Int32,Uint16,Int16,Folat
        }
       private InputTypes _inputtype; 
       public InputTypes InputType
        {
            get
            {
                return _inputtype;
              
          }
            set
            {
                _inputtype = value;

            }
        }//创建一个输入类型的属性 
       public int Address { get; set; }
        public NumTextBox()
        {
            this.MaxLines = 1;
            this.PreviewKeyDown += NumTextBox_PreviewKeyDown;
            this.PreviewKeyUp+= NumTextBox_KeyUp;

        }

        private void NumTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            switch (_inputtype)
            {
                case InputTypes.Uint32:

                    if (e.Key == Key.Back || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                    break;
                case InputTypes.Int32://32位整数
                    if (Text.Length < 1)
                    {
                        if (e.Key == Key.Back || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.OemMinus||e.Key==Key.Subtract||e.Key>=Key.NumPad0&&e.Key<=Key.NumPad9)
                            e.Handled = false;
                        else
                            e.Handled = true;
                    }
                    else
                    {
                        if (e.Key == Key.Back || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                            e.Handled = false;
                        else
                            e.Handled = true;
                    }
                    break;
                case InputTypes.Uint16://16位正整数

                    if (e.Key == Key.Back || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                    break;
                case InputTypes.Int16:
                    if (Text.Length < 1)
                    {
                        if (e.Key == Key.Back || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key==Key.OemMinus || e.Key == Key.Subtract || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                            e.Handled = false;
                        else
                            e.Handled = true;
                    }
                    else
                    {
                        if (e.Key == Key.Back || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                            e.Handled = false;
                        else
                            e.Handled = true;
                    }
                    break;
                case InputTypes.Folat:
                    if (e.Key==Key.OemPeriod||e.Key==Key.Decimal||e.Key == Key.Back || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.OemMinus || e.Key == Key.Subtract || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                    break;
                default:
                    break;
            }

        }//限制数据输入

        private void NumTextBox_KeyUp(object sender, KeyEventArgs e)//检测数据正确性
        {
            switch (_inputtype)
            {
                case InputTypes.Uint32:
                    if (Text.Length >= 10)
                    {
                        try
                        {
                            Convert.ToUInt32(Text);
                        }
                        catch
                        {
                            MessageBox.Show("取值范围0~4294967295");
                            Text = null;
                            return;
                        }

                    }
                    break;
                case InputTypes.Int32:
                    if (Text.Length >= 10)
                    {
                        try
                        {
                            Convert.ToInt32(Text);
                        }
                        catch
                        {
                            MessageBox.Show("取值范围-2147483648~-2147483647");
                            Text = null;
                            return;
                        }

                    }
                    break;
                case InputTypes.Uint16:
                    if (Text.Length >= 5)
                        try
                        {
                            Convert.ToUInt16(Text);

                        }
                        catch
                        {
                            MessageBox.Show("取值范围0~65535");
                            Text = null;
                            return;
                        }
                    break;
                case InputTypes.Int16:
                   
                    if (Text.Length >= 5)
                    {
                        try
                        {
                            Convert.ToInt16(Text);
                        }
                        catch
                        {
                            MessageBox.Show("取值范围-32768~32767");
                            Text = null;
                            return;
                        }

                    }
                    break;
                case InputTypes.Folat:
                    if (Text.Length >= 10)
                    {
                        try
                        {
                          float s=  float.Parse(Text);
                        }
                        catch
                        {
                            MessageBox.Show("取值范围-1e+009~1e+009");
                            Text = string.Empty;
                            return;
                        }

                    }
                    break;
            }
        }
    }
}
