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

using Mvvm_test1.ViewModel;

namespace Mvvm_test1
{
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : Window
    {
      
        private Start start;

        public Main(Start start)
        {
          
            this.start = start;
            InitializeComponent();
            DataContext = new MainVM(this);
           
        }

        private void window_Closed(object sender, EventArgs e)
        {
        Modbus_Rtu.Modbus_Rtu_.modbus_rtu.ColsedPort();
                
                start.Show();
        }
    }
}
