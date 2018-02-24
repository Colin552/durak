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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Durak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Card myCard = new Card((Suit)0, (Rank)0);
            
            Image myImage = new Image();
            Grid myGrid = windowGrid;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Durak;component/Resources/10_of_clubs.png");
            bitmap.EndInit();
            myImage.Source = bitmap;
            myImage.Stretch = Stretch.Fill;
            myImage.Width = 100;
            myImage.Height = 154;
            windowGrid.Children.Add(myImage);
            myImage.Margin = new Thickness(10,10,0,0);
            Grid.SetRow(myImage, 1);         
        }
    }
}
