using Calculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.Commands
{
    internal class ButtonPressedCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        // Constructor to initialize the view model
        public ButtonPressedCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        // Event that is triggered when the ability to execute the command changes
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Method to determine if the command can be executed
        public bool CanExecute(object? parameter)
        {
            // Add logic to determine if the command can be executed if needed
            return true;
        }

        // Method to execute the command
        public void Execute(object? parameter)
        {
            if (parameter is string button)
            {
                _viewModel.GetPressedButton(button);
            }
        }
    }
}