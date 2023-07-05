using System.Windows.Input;

namespace Arm64.DesktopApp.WPF.ViewModels
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action<object?> action;

        public SimpleCommand(Action<object?> action)
        {
            this.action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
            {
                action(parameter);
            }
        }
    }
}
