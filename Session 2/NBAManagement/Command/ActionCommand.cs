using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.Command
{
    public class ActionCommand : ICommand 
    {
        private readonly Action onExecute;
        public ActionCommand(Action onExecute)
        {
            this.onExecute = onExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            onExecute.Invoke();
        }
    }
}
