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

using System.Windows.Threading;

namespace Presents_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int maxItems = 5;
        int currentItems = 0;

        Random rand = new Random();

        int score = 0;
        int missed = 0;

        Rect playerHitBox;

        DispatcherTimer gameTimer = new DispatcherTimer();

        List<Rectangle> itemsToRemove = new List<Rectangle>();

        ImageBrush playerImage = new ImageBrush();
        ImageBrush backgroundImage = new ImageBrush();

        int itemSpeed = 15;

        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/netLeft.png"));
            player1.Fill = playerImage;


        }

        private void GameEngine(object sender, EventArgs e)
        {
            scoreText.Content = "Caught: " + score;
            missedText.Content = "Missed " + missed;


            if (currentItems < maxItems)
            {
                MakePresents();
                currentItems++;
                itemsToRemove.Clear();
            }


            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "drops")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + itemSpeed);

                    if (Canvas.GetTop(x) > 720)
                    {
                        itemsToRemove.Add(x);
                        currentItems--;
                        missed++;
                    }

                    Rect presentsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    playerHitBox = new Rect(Canvas.GetLeft(player1), Canvas.GetTop(player1), player1.Width, player1.Height);

                    if (playerHitBox.IntersectsWith(presentsHitBox))
                    {
                        itemsToRemove.Add(x);
                        currentItems--;
                        score++;
                    }
                }
            }


            foreach (var i in itemsToRemove)
            {
                MyCanvas.Children.Remove(i);
            }

            if (score < 40)
            {
                itemSpeed = 15;
            }

            if (missed > 20)
            {
                gameTimer.Stop();
                MessageBox.Show("You Lost!!!" + Environment.NewLine + "You Scored: " + score + Environment.NewLine + "Click ok to play again");
                ResetGame();
            }

            if (score > 40)
            {
                gameTimer.Stop();
                MessageBox.Show("Congratulations! You Win!" + Environment.NewLine + "You Caught: " + (score - 1) + " Presents!" + Environment.NewLine + "Click OK to play again");
                ResetGame();
            }


        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Stop();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            Point position = e.GetPosition(this);

            double pX = position.X;

            Canvas.SetLeft(player1, pX - 10);

            if (Canvas.GetLeft(player1) < 260)
            {
                playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/netLeft.png"));
            }
            else
            {
                playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/netRight.png"));
            }

        }

        private void ResetGame()
        {
            // Reset game variables
            score = 0;
            missed = 0;
            currentItems = 0;
            itemSpeed = 20;

            // Clear any existing game objects (e.g., presents on the canvas)
            MyCanvas.Children.Clear();

            // Reinitialize the player and any other elements
            player1 = new Rectangle
            {
                Width = 80,
                Height = 80,
                Fill = playerImage,
                Tag = "player"
            };
            Canvas.SetLeft(player1, 214);
            Canvas.SetTop(player1, 591);
            MyCanvas.Children.Add(player1);

            // Re-add score and missed labels to the canvas
            MyCanvas.Children.Add(scoreText);
            MyCanvas.Children.Add(missedText);

            // Restart the game timer
            gameTimer.Start();

            // Focus the canvas again
            MyCanvas.Focus();
        }


        private void MakePresents()
        {
            ImageBrush presents = new ImageBrush();

            int i = rand.Next(1, 6);

            switch (i)
            {
                case 1:
                    presents.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/present_01.png"));
                    break;
                case 2:
                    presents.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/present_02.png"));
                    break;
                case 3:
                    presents.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/present_03.png"));
                    break;
                case 4:
                    presents.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/present_04.png"));
                    break;
                case 5:
                    presents.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/present_05.png"));
                    break;
                case 6:
                    presents.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/present_06.png"));
                    break;
            }

            Rectangle newRec = new Rectangle
            {
                Tag = "drops",
                Width = 50,
                Height = 50,
                Fill = presents
            };

            // Random X placement logic for center and right side bias
            double leftPosition;
            int randomValue = rand.Next(0, 100); // Random value from 0 to 99

            if (randomValue < 10) // 10% chance to appear on the left side
            {
                leftPosition = rand.Next(0, (int)(MyCanvas.ActualWidth / 3) - (int)newRec.Width); // Left side (first third)
            }
            else if (randomValue < 70) // 60% chance to appear in the middle
            {
                leftPosition = rand.Next((int)(MyCanvas.ActualWidth / 3), (int)(MyCanvas.ActualWidth * 2 / 3) - (int)newRec.Width); // Middle third
            }
            else // 30% chance to appear on the right side
            {
                leftPosition = rand.Next((int)(MyCanvas.ActualWidth * 2 / 3), (int)(MyCanvas.ActualWidth - newRec.Width)); // Right side (last third)
            }

            // Set the horizontal (X) position with the selected bias
            Canvas.SetLeft(newRec, leftPosition);
            // Set a random vertical position above the canvas
            Canvas.SetTop(newRec, rand.Next(60, 150) * -1); // Negative Y to start above the canvas

            MyCanvas.Children.Add(newRec);
        }

    }
}