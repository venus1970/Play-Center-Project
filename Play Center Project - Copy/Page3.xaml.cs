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

namespace Play_Center_Project
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }
        private void ShowDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Data to display about the user
            string userData = "Name: Aharoni Tzila\n" +
                              "Age: 45\n" +
                              "Hobbies: Coding, Gaming, Reading\n" +
                              "Favorite Technology: .NET Framework\n" +
                              "Contact: aharonitzila@example.com";

            // Set the tooltip dynamically
            ToolTip toolTip = new ToolTip();
            TextBlock toolTipText = new TextBlock
            {
                Text = userData,
                TextWrapping = TextWrapping.Wrap
            };
            toolTip.Content = toolTipText;
            ShowDataButton.ToolTip = toolTip;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close(); // Close the current window hosting the page
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            CurrencyConverter.MainWindow mainWindow = new();
            mainWindow.Show();

        }

        private void ProfileImage_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close(); // Close the current window hosting the page

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrencyConverter.MainWindow mainWindow = new();
            mainWindow.Show();

        }
    }
}
