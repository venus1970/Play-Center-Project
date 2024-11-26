using Quiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Quiz.Commands
{
    internal class ButtonPressed : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private MainViewModel _mainViewModel;

        public MainViewModel mainViewModel
        {
            get { return _mainViewModel; }
            set { _mainViewModel = value; }
        }
        public ButtonPressed(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            mainViewModel.CheckPressedButton(parameter.ToString());
        }
    }
}
