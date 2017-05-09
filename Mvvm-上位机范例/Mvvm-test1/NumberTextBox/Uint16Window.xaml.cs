using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mvvm_test1.NumberTextBox
{
    /// <summary>
    /// Uint16Window.xaml 的交互逻辑
    /// </summary>
    public partial class Uint16Window : Window
    {
        public Uint16Window()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Modbus_Rtu.Modbus_Rtu_.modbus_rtu.WriteWord(1, numTextBox.Address, numTextBox.Text);
            this.Close();
        }
    }
}
