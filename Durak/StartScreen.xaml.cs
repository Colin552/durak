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

namespace Durak
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            int numOfCards = 0;

            if (opt20.IsChecked == true)
                numOfCards = 20;
            else if (opt36.IsChecked == true)
                numOfCards = 36;
            else if(opt52.IsChecked == true)
                numOfCards = 52;
            else
                numOfCards = 36;

            MainWindow mainWindow = new MainWindow(numOfCards);
            mainWindow.Show();
            this.Close();
        }
    }
}
