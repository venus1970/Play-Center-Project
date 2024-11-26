Play Center Project
Play Center Project is a WPF (Windows Presentation Foundation) application that provides a collection of engaging and interactive games for users. The platform features various games designed to challenge problem-solving skills, enhance cognitive abilities, and provide entertainment. The user-friendly interface allows players to easily navigate and enjoy their experience.

This project is built using C# and XAML.

Features:
User Interface: Customizable UI with a vibrant theme.


Games Available:
1) Calculator: A fun, interactive calculator game where players solve math problems under time pressure.
2) Save Presents Game:  A fun catching game where players must catch falling presents in a basket to earn points. 
3) Currency Converter: A game that challenges players to convert currency between different countries accurately and quickly.
4) Balloon Popping Game: A fast-paced game where players pop balloons that appear on the screen to score points.
5) Snake: A classic arcade game where players control a snake, eating food to grow longer while avoiding collisions.
6) Tic Tac Toe: A classic two-player game where players take turns marking X's and O's on a 3x3 grid, aiming to get three of their marks in a row, column, or diagonal. The first player to do so wins.
7) Tetris: Classic video puzzle game where players arrange falling blocks to form complete lines and clear the screen.
8) Quiz: A series of quiz questions. A trivia quiz game with a variety of questions on different topics when you will choose from 4 options one answer.
9) Car Racing Game: A racing game where players control a car and avoid obstacles while racing for the highest score. 

Navigation: Easy-to-use navigation with images that link to game instructions or details.
Animations and Effects: Beautiful transitions, hover effects, and shadow effects on images and buttons.
Requirements:
.NET SDK: Make sure you have the latest version of the .NET SDK installed (preferably .NET 6.0 or .NET 7.0 for compatibility with WPF).
Visual Studio: It's recommended to use Visual Studio with the WPF development workload installed.

Getting Started:
1. Clone the Repository:

git clone https://github.com/Venus1970/Play-Center-Project.git
cd Play-Center-Project

2. Open the Project:
Open the project in Visual Studio or your preferred IDE that supports C# and WPF development.

3. Restore NuGet Packages:
If any required packages are missing, Visual Studio should prompt you to restore them automatically. You can also restore packages manually by running:

dotnet restore

4. Build and Run the Application:
To build and run the application, simply press F5 or click Start Debugging in Visual Studio.

Project Structure:
MainWindow.xaml: The main window of the Play Center app where the UI is defined, including the top section, middle grid for games, and bottom section.
Images: All the images used in the game thumbnails and background can be found in the /Images/ directory. These are referenced in the XAML files.
Assets: For game-related assets like instructions or game data, you might have text files or other resources in the /Assets/ folder.
Event Handlers: Various event handlers are defined for images (MouseLeftButtonUp, MouseLeftButtonDown) to navigate to specific game instructions or details.
How to Play:
When you launch the app, you will be presented with a grid of game thumbnails.
Hover over each image to view the title and click on any image to view more details about the game or start playing.
The application supports easy navigation and ensures a smooth transition between game details and the main interface.
Troubleshooting:
Images not loading: Ensure that the images are present in the /Images/ folder and correctly referenced in the XAML file.
App crashes or lags: Check the output for any error messages and ensure all required resources (like images and text files) are available.
Screen resolution issues: The UI is designed to be responsive, but you may need to adjust the layout if you are using non-standard screen resolutions.
Contributing:
We welcome contributions to improve this project. If you want to contribute, please follow these steps:

Fork the repository.
Clone your fork to your local machine.
Create a new branch (git checkout -b feature/your-feature).
Make your changes and commit them (git commit -am 'Add new feature').
Push to your branch (git push origin feature/your-feature).
Open a pull request.

Contact:
Developer: Aharoni Tzila
Email: zilaalkobi@gmail.com
GitHub: https://github.com/Venus1970
Notes for the Readme:
Images and Resources: The Images/ folder should contain all the images referenced in the XAML file. 

Event Handlers: In the provided XAML, event handlers such as Image1_Click, Image2_Click, etc., are invoked when an image is clicked. 

Navigation: If your app is designed to use multiple pages, the Frame element in the XAML suggests that the app uses navigation to load different pages. The MyFrame is used for navigating between different game pages.

I hope this README file gives users and developers all the information needed to understand, install, and contribute to my Play Center Project. 



