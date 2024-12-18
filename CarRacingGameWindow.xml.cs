﻿< Window x: Class = "Car_Racing_Game_WPF.MainWindow"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns: d = "http://schemas.microsoft.com/expression/blend/2008"
        xmlns: mc = "http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns: local = "clr-namespace:Car_Racing_Game_WPF"
        mc: Ignorable = "d"
        Title = "Top Down Racing Game" Height = "517" Width = "525" >
    < Canvas Background = "Gray" Name = "myCanvas" Focusable = "True" KeyDown = "OnKeyDown" KeyUp = "OnKeyUP" >

        < Rectangle Height = "106" Width = "20" Fill = "White" Tag = "roadMarks" Canvas.Left = "237" Canvas.Top = "-152" />
        < Rectangle Height = "106" Width = "20" Fill = "White" Tag = "roadMarks" Canvas.Left = "237" Canvas.Top = "10" />
        < Rectangle Height = "106" Width = "20" Fill = "White" Tag = "roadMarks" Canvas.Left = "237" Canvas.Top = "176" />
        < Rectangle Height = "106" Width = "20" Fill = "White" Tag = "roadMarks" Canvas.Left = "237" Canvas.Top = "348" />


        < Rectangle Tag = "Car" Height = "80" Width = "55" Fill = "Blue" Canvas.Left = "90" Canvas.Top = "56" />
        < Rectangle Tag = "Car" Height = "80" Width = "55" Fill = "Purple" Canvas.Left = "381" Canvas.Top = "286" />


        < Rectangle Name = "player" Height = "80" Width = "55" Fill = "Yellow" Canvas.Left = "222" Canvas.Top = "374" />

        < Label Name = "scoreText" Content = "Survived: 00 Seconds" FontSize = "18" FontWeight = "Bold" />


    </ Canvas >
</ Window >

