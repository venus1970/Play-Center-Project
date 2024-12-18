﻿using System;
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
using System.Windows.Threading; // import the threading namespace first, this way we can use the dispatcher time inside of the c# script

namespace Car_Racing_Game_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer(); // create a new instance of the dispatcher time called gameTimer
        List<Rectangle> itemRemover = new List<Rectangle>(); // make a new list called item remove, this list will be used to remove any unused rectangles in the game 

        Random rand = new Random(); // make a new instance of the random class called rand

        ImageBrush playerImage = new ImageBrush(); // create a new image brush for the player
        ImageBrush starImage = new ImageBrush(); // create a new image brush for the star

        Rect playerHitBox; // this rect object will be used to calculate the player hit area with other objects

        // set the game integers including, speed for the traffic and road markings, player speed, car numbers, star counter and power mode counter
        int speed = 15;
        int playerSpeed = 10;
        int carNum;
        int starCounter = 30;
        int powerModeCounter = 200;

        // create two doubles one for score and other called i, this one will be used to animate the player car when we reach the power mode
        double score;
        double i;

        // we will need 4 boolean altogether for this game, since all of them will be false at the start we are defining them in one line. 
        bool moveLeft, moveRight, gameOver, powerMode;



        public MainWindow()
        {
            InitializeComponent();

            myCanvas.Focus(); // Set focus to canvas

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            // Set the MP3 file for the racing sound
            RacingSound.Source = new Uri("pack://application:,,,/Assets/racing.mp3");  // Ensure 'Assets' folder is in project
            RacingSound.MediaFailed += (sender, e) => { /* Handle any media errors */ };
            RacingSound.Play();  // Start the racing sound
            RacingSound.IsMuted = false; // Ensure it's not muted
            RacingSound.MediaEnded += (sender, e) => RacingSound.Play(); // Loop the sound

            // Set the MP3 file for the crash sound
            CrashSound.Source = new Uri("pack://application:,,,/Assets/crash.mp3");  // Ensure 'Assets' folder is in project

            StartGame(); // Run the start game function
        }

        private void GameLoop(object sender, EventArgs e)
        {
            score += .05; // increase the score by .5 each tick of the timer

            starCounter -= 1; // reduce 1 from the star counter each tick

            scoreText.Content = "Survived " + score.ToString("#.#") + " Seconds"; // this line will show the seconds passed in decimal numbers in the score text label

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height); // assign the player hit box to the player

            // below are two if statements that are checking the player can move or right in the scene. 
            if (moveLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }

            // if the star counter integer goes below 1 then we run the make star function and also generate a random number inside of the star counter integer
            if (starCounter < 1)
            {
                MakeStar();
                starCounter = rand.Next(600, 900);
            }

            // below is the main game loop, inside of this loop we will go through all of the rectangles available in this game
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                // first we search through all of the rectangles in this game

                // then we check if any of the rectangles has a tag called road marks
                if ((string)x.Tag == "roadMarks")
                {
                    // if we find any of the rectangles with the road marks tag on it then 

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed); // move it down using the speed variable

                    // if the road marks goes below the screen then move it back up top of the screen
                    if (Canvas.GetTop(x) > 510)
                    {
                        Canvas.SetTop(x, -152);
                    }

                } // end of the road marks if statement

                // if we find a rectangle with the car tag on it
                if ((string)x.Tag == "Car")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed); // move the rectangle down using the speed variable

                    // if the car has left the scene then run then run the change cars function with the current x rectangle inside of it
                    if (Canvas.GetTop(x) > 500)
                    {
                        ChangeCars(x);
                    }

                    // create a new rect called car hit box and assign it to the x which is the cars rectangle
                    Rect carHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // if the player hit box and the car hit box collide and the power mode is ON
                    if (playerHitBox.IntersectsWith(carHitBox) && powerMode == true)
                    {
                        // run the change cars function with the cars rectangle X inside of it
                        ChangeCars(x);
                    }
                    else if (playerHitBox.IntersectsWith(carHitBox) && powerMode == false)
                    {
                        // if the power is OFF and car and the player collide then

                        gameTimer.Stop(); // stop the game timer
                        scoreText.Content += " Press Enter to replay"; // add this text to the existing text on the label
                        gameOver = true; // set game over boolean to true

                        // Play crash sound when the player hits a car
                        CrashSound.Play();
                    }

                } // end of car if statement

                // if we find a rectangle with the star tag on it
                if ((string)x.Tag == "star")
                {
                    // move it down the screen 5 pixels at a time
                    Canvas.SetTop(x, Canvas.GetTop(x) + 5);

                    // create a new rect with for the star and pass in the star X values inside of it
                    Rect starHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // if the player and the star collide then
                    if (playerHitBox.IntersectsWith(starHitBox))
                    {
                        // add the star to the item remover list
                        itemRemover.Add(x);

                        // set power mode to true
                        powerMode = true;

                        // set power mode counter to 200
                        powerModeCounter = 200;

                    }

                    // if the star goes beyond 400 pixels then add it to the item remover list
                    if (Canvas.GetTop(x) > 400)
                    {
                        itemRemover.Add(x);
                    }

                } // end of start if statement
            } // end of for each loop

            // if the power mode is true
            if (powerMode == true)
            {
                powerModeCounter -= 1; // reduce 1 from the power mode counter 
                // run the power up function
                PowerUp();
                // if the power mode counter goes below 1 
                if (powerModeCounter < 1)
                {
                    // set power mode to false
                    powerMode = false;
                }
            }
            else
            {
                // if the mode is false then change the player car back to default and also set the background to gray
                playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/playerImage.png"));
                myCanvas.Background = Brushes.Blue;
            }

            // each item we find inside of the item remove we will remove it from the canvas
            foreach (Rectangle y in itemRemover)
            {
                myCanvas.Children.Remove(y);
            }

            // below are the score and speed configurations for the game
            // as you progress in the game you will score higher and traffic speed will go up

            if (score >= 10 && score < 20)
            {
                speed = 12;
            }

            if (score >= 20 && score < 30)
            {
                speed = 14;
            }
            if (score >= 30 && score < 40)
            {
                speed = 16;
            }
            if (score >= 40 && score < 50)
            {
                speed = 18;
            }
            if (score >= 50 && score < 80)
            {
                speed = 22;
            }

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // key down function will listen for you the user to press the left or right key and it will change the designated boolean to true

            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }

        }

        private void OnKeyUP(object sender, KeyEventArgs e)
        {
            // when the player releases the left or right key it will set the designated boolean to false

            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }

            // in this case we will listen for the enter key as well but for this to execute we will need the game over boolean to be true
            if (e.Key == Key.Enter && gameOver == true)
            {
                // if both of these conditions are true then we will run the start game function
                StartGame();
            }


        }

        private void StartGame()
        {
            // thi sis the start game function, this function to reset all of the values back to their default state and start the game

            speed = 8; // set speed to 8
            gameTimer.Start(); // start the timer

            // set all of the boolean to false
            moveLeft = false;
            moveRight = false;
            gameOver = false;
            powerMode = false;

            // set score to 0
            score = 0;
            // set the score text to its default content
            scoreText.Content = "Survived: 0 Seconds";
            // set up the player image and the star image from the images folder
            playerImage.
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/playerImage.png"));
            starImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/star.png"));
            // assign the player image to the player rectangle from the canvas
            player.Fill = playerImage;
            // set the default background color to gray
            myCanvas.Background = Brushes.Blue;

            // run a initial for each loop to set up the cars and remove any star in the game

            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                // if we find any rectangle with the car tag on it then we will
                if ((string)x.Tag == "Car")
                {
                    // set a random location to their top and left position
                    Canvas.SetTop(x, (rand.Next(100, 400) * -1));
                    Canvas.SetLeft(x, rand.Next(0, 430));
                    // run the change cars function
                    ChangeCars(x);
                }

                // if we find a star in the beginning of the game then we will add it to the item remove list
                if ((string)x.Tag == "star")
                {
                    itemRemover.Add(x);
                }

            }
            // clear any items inside of the item remover list at the start
            itemRemover.Clear();


        }

        private void ChangeCars(Rectangle car)
        {
            // we want the game to change the traffic car images as they leave the scene and come back to it again

            carNum = rand.Next(1, 6); // to start lets generate a random number between 1 and 6

            ImageBrush carImage = new ImageBrush(); // create a new image brush for the car image 

            // the switch statement below will see what number have generated for the car num integer and 
            // based on that number it will assign a different image to the car rectangle
            switch (carNum)
            {
                case 1:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car1.png"));
                    break;
                case 2:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car2.png"));
                    break;
                case 3:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car3.png"));
                    break;
                case 4:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car4.png"));
                    break;
                case 5:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car5.png"));
                    break;
                case 6:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car6.png"));
                    break;
            }

            car.Fill = carImage; // assign the chosen car image to the car rectangle

            // set a random top and left position for the traffic car
            Canvas.SetTop(car, (rand.Next(100, 400) * -1));
            Canvas.SetLeft(car, rand.Next(0, 430));
            Canvas.SetRight(car, rand.Next(0, 430));


        }

        private void PowerUp()
        {
            // this is the power up function, this function will run when the player collects the star in the game

            i += .5; // increase i by .5 


            // if i is greater than 4 then reset i back to 1
            if (i > 4)
            {
                i = 1;
            }

            // with each increment of the i we will change the player image to one of the 4 images below

            switch (i)
            {
                case 1:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powerMode1.png"));
                    break;
                case 2:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powerMode2.png"));
                    break;
                case 3:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powerMode3.png"));
                    break;
                case 4:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powerMode4.png"));
                    break;
            }

            // change the background to light coral color
            myCanvas.Background = Brushes.Green;



        }

        private void MakeStar()
        {
            // this is the make star function
            // this function will create a rectangle, assign the star image to and place it on the canvas

            // creating a new star rectangle with its own properties inside of it
            Rectangle newStar = new Rectangle
            {
                Height = 50,
                Width = 50,
                Tag = "star",
                Fill = starImage
            };

            // set a random left and top position for the star
            Canvas.SetLeft(newStar, rand.Next(0, 430));
            Canvas.SetTop(newStar, (rand.Next(100, 400) * -1));

            // finally add the new star to the canvas to be animated and to interact with the player
            myCanvas.Children.Add(newStar);



        }
    }
}
