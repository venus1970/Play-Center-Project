﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Play_Center_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
                this.Width = 1024;
                this.Height = 720;

                IsMaximize = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;

                IsMaximize = true;
            }
        }
        private void Image1_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page1());
        }

        private void Image2_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page2());

        }

        private void Image3_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page3());

        }
        private void Image4_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page4());

        }

        private void Image5_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page5());

        }

        private void Image6_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page6());

        }

        private void Image7_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page7());

        }

        private void Image8_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page8());

    
        }

        private void Image9_Click(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Navigate(new Page9());
            

        }

        private void MyFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
        
        
    }   
       
}