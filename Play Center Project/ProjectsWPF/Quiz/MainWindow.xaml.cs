using CommunityToolkit.Mvvm.Messaging;
using Quiz.Messages;
using Quiz.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quiz
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel();

            // Register to receive messages via WeakReferenceMessenger
            WeakReferenceMessenger.Default.Register<EndOfGame>(this, (recipient, message) => CloseTheGame(message));

            InitializeComponent();
        }

        // Method to handle the EndOfGame message
        void CloseTheGame(EndOfGame obj)
        {
            this.Close();
        }
    }
}