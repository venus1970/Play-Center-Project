using Calculator.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _keyPressedString;
        public string KeyPressedString
        {
            get { return _keyPressedString; }
            set
            {
                if (_keyPressedString != value)
                {
                    _keyPressedString = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _enteredNumber;
        public string EnteredNumber
        {
            get { return _enteredNumber; }
            set
            {
                if (_enteredNumber != value)
                {
                    _enteredNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private ICommand? _buttonPressedCommand;
        public ICommand? ButtonPressedCommand
        {
            get { return _buttonPressedCommand; }
            set
            {
                if (_buttonPressedCommand != value)
                {
                    _buttonPressedCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> _enteredKeys;
        private double _number;

        public string ErrorMessage { get; private set; }

        private bool _firstNumberEntered;
        private bool _equalToFlag;
        private bool _functionPressed;
        private string _selectedFunction;
        public string PreviousEnteredKey { get; set; }

        public MainViewModel()
        {
            EnteredNumber = "0";
            KeyPressedString = string.Empty;
            _enteredKeys = new List<string>();
            _number = 0;
            _firstNumberEntered = true;
            _equalToFlag = true;
            _functionPressed = false;
            _selectedFunction = string.Empty;
            PreviousEnteredKey = string.Empty;
            ButtonPressedCommand = new ButtonPressedCommand(this);
        }

        private void UpdateEnteredKeyOnGui()
        {
            KeyPressedString = string.Join(string.Empty, _enteredKeys);
        }

        private void PerformOperation(Func<double, double, double> operation)
        {
            _number = operation(_number, Convert.ToDouble(EnteredNumber));
            EnteredNumber = _number.ToString();
        }

        private void ToggleSign()
        {
            if (double.TryParse(EnteredNumber, out var number))
            {
                EnteredNumber = (-number).ToString();
            }
        }

        private void CalculatePercentage()
        {
            if (double.TryParse(EnteredNumber, out var number))
            {
                EnteredNumber = (number / 100).ToString();
            }
        }

        private void SquareRoot()
        {
            if (double.TryParse(EnteredNumber, out var number))
            {
                EnteredNumber = Math.Sqrt(number).ToString();
            }
        }

        private void HandleParentheses(string pressedButton)
        {
            // Implement parentheses logic
        }

        private void Addition() => PerformOperation((a, b) => a + b);
        private void Subtraction() => PerformOperation((a, b) => a - b);
        private void Multiplication() => PerformOperation((a, b) => a * b);
        private void Division()
        {
            // Check if the denominator (EnteredNumber) is zero
            if (double.TryParse(EnteredNumber, out var divisor) && divisor == 0)
            {
                // Show an alert message when division by zero is attempted
                MessageBox.Show("Warning: Division by zero is not allowed.", "Division by Zero", MessageBoxButton.OK, MessageBoxImage.Warning);

                // Set the result to "Infinity"
                EnteredNumber = "Error";  // Display "Infinity" in the entered number field
                _number = double.PositiveInfinity;  // Internal number set to positive infinity
            }
            else
            {
                // Perform normal division if denominator is not zero
                PerformOperation((a, b) => a / b);
            }
        }


        private void EqualTo()
        {
            _enteredKeys.Clear();
            _enteredKeys.Add(EnteredNumber);
            _equalToFlag = false;
        }

        private void Clear()
        {
            _enteredKeys.Clear();
            KeyPressedString = string.Empty;
            EnteredNumber = "0";
            _number = 0;
            _firstNumberEntered = true;
            _equalToFlag = false;
        }

        private bool IsNumberOrDot(string pressedButton)
        {
            return pressedButton == "0" || pressedButton == "1" || pressedButton == "2" || pressedButton == "3" ||
                   pressedButton == "4" || pressedButton == "5" || pressedButton == "6" || pressedButton == "7" ||
                   pressedButton == "8" || pressedButton == "9" || pressedButton == ".";
        }

        private bool PressedButtonOperator(string pressedButton)
        {
            if (IsNumberOrDot(pressedButton))
            {
                if (_equalToFlag)
                    Clear();

                _enteredKeys.Add(pressedButton);
                UpdateEnteredKeyOnGui();

                PreviousEnteredKey = pressedButton;

                if (_functionPressed)
                {
                    EnteredNumber = "0";
                    _functionPressed = false;
                }

                EnteredNumber = EnteredNumber == "0" ? pressedButton : EnteredNumber + pressedButton;

                return false;
            }
            else
            {
                return true;
            }
        }

        public void GetPressedButton(string pressedButton)
        {
            if (PressedButtonOperator(pressedButton))
            {
                if (PreviousEnteredKey != pressedButton && !"+-*/Clr".Contains(PreviousEnteredKey))
                {
                    if (_enteredKeys.Count == 0)
                    {
                        _enteredKeys.Add(EnteredNumber);
                        UpdateEnteredKeyOnGui();
                    }

                    if (_firstNumberEntered)
                    {
                        _number = Convert.ToDouble(EnteredNumber);
                        EnteredNumber = _number.ToString();
                        _firstNumberEntered = false;
                    }
                    else
                    {
                        switch (_selectedFunction)
                        {
                            case "Addition": Addition(); break;
                            case "Subtraction": Subtraction(); break;
                            case "Multiplication": Multiplication(); break;
                            case "Division": Division(); break;
                            case "EqualTo": EqualTo(); break;
                        }
                    }

                    switch (pressedButton)
                    {
                        case "+": SetOperation("Addition", pressedButton); break;
                        case "-": SetOperation("Subtraction", pressedButton); break;
                        case "*": SetOperation("Multiplication", pressedButton); break;
                        case "/": SetOperation("Division", pressedButton); break;
                        case "=": SetOperation("EqualTo", pressedButton); break;
                        case "Clr": Clear(); break;
                        case "±": ToggleSign(); break;
                        case "%": CalculatePercentage(); break;
                        case "√": SquareRoot(); break;                       
                    }
                }
                else if ("+-*/Clr".Contains(PreviousEnteredKey))
                {
                    _enteredKeys.RemoveAt(_enteredKeys.Count - 1);
                    _enteredKeys.Add(pressedButton);
                    UpdateEnteredKeyOnGui();
                    PreviousEnteredKey = pressedButton;
                    _functionPressed = true;

                    switch (pressedButton)
                    {
                        case "+": _selectedFunction = "Addition"; break;
                        case "-": _selectedFunction = "Subtraction"; break;
                        case "*": _selectedFunction = "Multiplication"; break;
                        case "/": _selectedFunction = "Division"; break;
                        case "=": _selectedFunction = "EqualTo"; break;
                        case "Clr": Clear(); break;
                    }
                }
            }
        }

        private void SetOperation(string operation, string pressedButton)
        {
            _selectedFunction = operation;
            _enteredKeys.Add(pressedButton);
            UpdateEnteredKeyOnGui();
            PreviousEnteredKey = pressedButton;
            _functionPressed = true;
        }
    }
}

