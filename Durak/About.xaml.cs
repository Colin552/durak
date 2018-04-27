/*
 * Authors: Calvin Lapp, Colin Strong, Elizabeth Welch
 * Date: 4/27/2018
 * Descritpion: About form
 * 
 */
using System.Windows;

namespace Durak
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        /// <summary>
        /// Initialize the form
        /// </summary>
        public About()
        {
            InitializeComponent();
            PrintAbout();
        }
        /// <summary>
        /// Fill in the about label
        /// </summary>
        private void PrintAbout()
        {
            string about = "";

            about = "Created for OOP4200 at Durham College, Winter 2018\n\n" +
                "Authors: Colin Strong, Calvin Lapp & Elizabeth Welch\n\n" +
                "Version 1.0 (2018/04/27)";

            lblAbout.Content = about;
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}