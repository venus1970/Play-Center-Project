using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tic_Tac_Toe_App
{
    public partial class MainWindow : Window
    {
        private string value = "X";
        private int xWins = 0;
        private int oWins = 0;
        private static readonly Brush DEFAULTBRUSH = new SolidColorBrush(Color.FromArgb(255, 142, 142, 166));

        public MainWindow()
        {
            InitializeComponent();
        }

        // Menu Click Events
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            xWins = 0;
            oWins = 0;
            lbXWins.Content = "X: 0";
            lbOWins.Content = "O: 0"; // Corrected label for O wins
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a Tic Tac Toe App.\nPlay and enjoy.\nDate: 28.10.2024", "Tic_Tac_Toe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Button Events
        private void bt_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            bt.Foreground = Brushes.Black;
            bt.IsEnabled = false;
            bt.Content = value;

            // Check all winning conditions
            if (IsWin(btA1, btA2, btA3) || IsWin(btB1, btB2, btB3) || IsWin(btC1, btC2, btC3) ||
                IsWin(btA1, btB1, btC1) || IsWin(btA2, btB2, btC2) || IsWin(btA3, btB2, btC3) ||
                IsWin(btA1, btB2, btC3) || IsWin(btA3, btB2, btC1))
            {
                GameOver(value);
            }
            else if (!btA1.IsEnabled && !btA2.IsEnabled && !btA3.IsEnabled &&
                     !btB1.IsEnabled && !btB2.IsEnabled && !btB3.IsEnabled &&
                     !btC1.IsEnabled && !btC2.IsEnabled && !btC3.IsEnabled)
            {
                GameOver(""); // Trigger no winner on a full board
            }
            else
            {
                value = value == "X" ? "O" : "X"; // Switch player
            }
        }

        private void GameOver(string who)
        {
            if (lbWinner.Visibility == Visibility.Visible) return;

            if (who == "X")
            {
                lbWinner.Content = "Player X Win!";
                lbXWins.Content = $"Player X: {++xWins}";
            }
            else if (who == "O")
            {
                lbWinner.Content = "Player O Win!";
                lbOWins.Content = $"Player O: {++oWins}"; // Corrected label for O wins
            }
            else
            {
                lbWinner.Content = "No winner!";
            }

            lbWinner.Visibility = Visibility.Visible;
            Wait1SecAndRestart();
        }

        private async void Wait1SecAndRestart()
        {
            await Task.Delay(1000);
            lbWinner.Visibility = Visibility.Hidden;
            ResetButtons();
        }

        private void ResetButtons()
        {
            ResetButton(btA1);
            ResetButton(btA2);
            ResetButton(btA3);
            ResetButton(btB1);
            ResetButton(btB2);
            ResetButton(btB3);
            ResetButton(btC1);
            ResetButton(btC2);
            ResetButton(btC3);
        }

        private void ResetButton(Button bt)
        {
            bt.Content = "";
            bt.IsEnabled = true;
            bt.Foreground = DEFAULTBRUSH;
        }

        private bool IsWin(Button bt1, Button bt2, Button bt3) =>
            !bt1.IsEnabled && bt1.Content == bt2.Content && bt1.Content == bt3.Content;

        private void bt_Enter(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            bt.Content = value;
        }

        private void bt_Leave(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            if (bt.IsEnabled)
                bt.Content = "";
        }

        // Link Events
        private void link_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c start www.youTube.com",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            });
        }
    }
}
