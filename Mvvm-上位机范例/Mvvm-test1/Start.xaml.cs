using System.Windows;
using Mvvm_test1.ViewModel;


namespace Mvvm_test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Start : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public Start()
        {
            InitializeComponent();
            DataContext = new StartVM(this);
        }

    }
}