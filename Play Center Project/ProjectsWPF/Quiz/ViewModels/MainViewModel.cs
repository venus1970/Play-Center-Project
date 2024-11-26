using CommunityToolkit.Mvvm.Messaging;
using Quiz.Commands;
using Quiz.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Quiz.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        string FilePath;
        List<string> Questions;
        List<string> Answers;
        List<string> Options;
        int QuestionNumber;
        int Score;

        private string _Question;
        private int CorrectAnswers;
        private int IncorrectAnswers;

        public string Question
        {
            get { return _Question; }
            set { _Question = value; OnPropertyChanged(nameof(Question)); }
        }

        private string _Option1;
        public string Option1
        {
            get { return _Option1; }
            set { _Option1 = value; OnPropertyChanged(nameof(Option1)); }
        }

        private string _Option2;
        public string Option2
        {
            get { return _Option2; }
            set { _Option2 = value; OnPropertyChanged(nameof(Option2)); }
        }

        private string _Option3;
        public string Option3
        {
            get { return _Option3; }
            set { _Option3 = value; OnPropertyChanged(nameof(Option3)); }
        }

        private string _Option4;
        public string Option4
        {
            get { return _Option4; }
            set { _Option4 = value; OnPropertyChanged(nameof(Option4)); }
        }

        private bool _AnswerButtonEnabled;
        public bool AnswerButtonEnabled
        {
            get { return _AnswerButtonEnabled; }
            set { _AnswerButtonEnabled = value; OnPropertyChanged(nameof(AnswerButtonEnabled)); }
        }

        private string _Button1Selected;
        public string Button1Selected
        {
            get { return _Button1Selected; }
            set { _Button1Selected = value; OnPropertyChanged(nameof(Button1Selected)); }
        }

        private string _Button2Selected;
        public string Button2Selected
        {
            get { return _Button2Selected; }
            set { _Button2Selected = value; OnPropertyChanged(nameof(Button2Selected)); }
        }

        private string _Button3Selected;
        public string Button3Selected
        {
            get { return _Button3Selected; }
            set { _Button3Selected = value; OnPropertyChanged(nameof(Button3Selected)); }
        }

        private string _Button4Selected;
        public string Button4Selected
        {
            get { return _Button4Selected; }
            set { _Button4Selected = value; OnPropertyChanged(nameof(Button4Selected)); }
        }

        private string _QuestionNumberString;
        public string QuestionNumberString
        {
            get { return _QuestionNumberString; }
            set { _QuestionNumberString = value; OnPropertyChanged(nameof(QuestionNumberString)); }
        }

        private string _ScoreString;
        public string ScoreString
        {
            get { return _ScoreString; }
            set { _ScoreString = value; OnPropertyChanged(nameof(ScoreString)); }
        }

        private ButtonPressed _buttonPressed;
        public ButtonPressed buttonPressed
        {
            get { return _buttonPressed; }
            set { _buttonPressed = value; }
        }

        public MainViewModel()
        {
            Score = 0;
            CorrectAnswers = 0;
            IncorrectAnswers = 0;
            ScoreString = Score.ToString();
            QuestionNumber = 0;
            AnswerButtonEnabled = false;
            QuestionNumberString = "Question " + (QuestionNumber + 1).ToString();

            Button1Selected = "LightGray";
            Button2Selected = "LightGray";
            Button3Selected = "LightGray";
            Button4Selected = "LightGray";

            // Get the base directory of the project (i.e., the root of your project)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine with the subdirectory where the files are located
            string questionsFilePath = Path.Combine(baseDirectory, @"Assets\Questions.txt");
            string answersFilePath = Path.Combine(baseDirectory, @"Assets\Answers.txt");
            string optionsFilePath = Path.Combine(baseDirectory, @"Assets\Options.txt");

            // Read all lines from the files
            this.Questions = File.ReadAllLines(questionsFilePath).ToList();
            this.Answers = File.ReadAllLines(answersFilePath).ToList();
            this.Options = File.ReadAllLines(optionsFilePath).ToList();

            buttonPressed = new ButtonPressed(this);

            GetNextQuestion();
            GetNextOptions();
        }

        bool IsAnswerCorrect()
        {
            if (Button1Selected == "Cyan" && Option1 != Answers[QuestionNumber])
                return false;

            if (Button2Selected == "Cyan" && Option2 != Answers[QuestionNumber])
                return false;

            if (Button3Selected == "Cyan" && Option3 != Answers[QuestionNumber])
                return false;

            if (Button4Selected == "Cyan" && Option4 != Answers[QuestionNumber])
                return false;

            return true;
        }

        void GameWon()
        {
            if (CorrectAnswers >= 20)
            {
                MessageBox.Show("Congratulations, you won the game!!! You answered 20 or more questions correctly.");
            }
            else
            {
                MessageBox.Show("Game Over. You didn't answer enough questions correctly.");
            }
            WeakReferenceMessenger.Default.Send(new EndOfGame());
        }

        void GameOver()
        {
            // Show the incorrect answer message with the correct answer
            MessageBox.Show($"Incorrect Answer\nCorrect answer is {Answers[QuestionNumber]}\nYour current score is {ScoreString}");

            // Increase the question number to move to the next question
            QuestionNumber++;

            // Check if the question number is within range
            if (QuestionNumber >= Questions.Count)
            {
                if (IncorrectAnswers >= 20)
                {
                    MessageBox.Show("You lost the game. You answered on most questions incorrectly.");
                }
                else if (CorrectAnswers >= 20)
                {
                    MessageBox.Show("Congratulations, you won the game!!! You answered on most questions correctly.");
                }
                else
                {
                    MessageBox.Show("The game is over. You need to improve your skills.");
                }

                WeakReferenceMessenger.Default.Send(new EndOfGame());
            }
            else
            {
                // If there are more questions, get the next question and options
                GetNextQuestion();
                GetNextOptions();
                QuestionNumberString = "Question " + (QuestionNumber + 1).ToString();

                // Reset button states and disable the Answer button
                Button1Selected = "LightGray";
                Button2Selected = "LightGray";
                Button3Selected = "LightGray";
                Button4Selected = "LightGray";
                AnswerButtonEnabled = false;
            }
        }

        void GetNextQuestion()
        {
            Question = Questions[QuestionNumber];
        }

        void GetNextOptions()
        {
            string[] options = Options[QuestionNumber].Split(',');

            Option1 = options[0];
            Option2 = options[1];
            Option3 = options[2];
            Option4 = options[3];
        }

        public void CheckPressedButton(string pressedButton)
        {
            switch (pressedButton)
            {
                case "Button1":
                    Button1Selected = "Cyan";
                    Button2Selected = "LightGray";
                    Button3Selected = "LightGray";
                    Button4Selected = "LightGray";
                    AnswerButtonEnabled = true;
                    break;

                case "Button2":
                    Button1Selected = "LightGray";
                    Button2Selected = "Cyan";
                    Button3Selected = "LightGray";
                    Button4Selected = "LightGray";
                    AnswerButtonEnabled = true;
                    break;

                case "Button3":
                    Button1Selected = "LightGray";
                    Button2Selected = "LightGray";
                    Button3Selected = "Cyan";
                    Button4Selected = "LightGray";
                    AnswerButtonEnabled = true;
                    break;

                case "Button4":
                    Button1Selected = "LightGray";
                    Button2Selected = "LightGray";
                    Button3Selected = "LightGray";
                    Button4Selected = "Cyan";
                    AnswerButtonEnabled = true;
                    break;

                case "AnswerButton":
                    // Check if the answer is correct
                    if (IsAnswerCorrect())
                    {
                        // Increment score and correct answer count if the answer is correct
                        CorrectAnswers++;
                        Score++;
                        ScoreString = Score.ToString();
                    }
                    else
                    {
                        // Increment incorrect answer count and game over if the answer is wrong
                        IncorrectAnswers++;
                        GameOver();
                    }

                    // Move to the next question
                    QuestionNumber++;

                    // If there are no more questions, display the game won message
                    if (QuestionNumber >= Questions.Count)
                    {
                        GameWon();
                    }
                    else
                    {
                        // Otherwise, load the next question and options
                        GetNextQuestion();
                        GetNextOptions();
                    }

                    // Update the question number string
                    QuestionNumberString = "Question " + (QuestionNumber + 1).ToString();

                    // Reset button states and disable the Answer button
                    Button1Selected = "LightGray";
                    Button2Selected = "LightGray";
                    Button3Selected = "LightGray";
                    Button4Selected = "LightGray";
                    AnswerButtonEnabled = false;
                    break;
            }
        }
    }
}
