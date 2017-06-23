using System;
using Command.Interfaces;
using Controller.Interfaces;
using DTO.Interfaces;

namespace DataCommand.Implementation
{
    public abstract class CRUDCommandBase : INotifiableCommand 
    {
        protected IDTOWrapper Source;
        protected IDTOCollection Target;
        protected ISimpleController Controller;
        protected Func<bool> Condition;

        protected CRUDCommandBase(IDTOWrapper source, IDTOCollection target, ISimpleController controller, Func<bool> condition)
        {
            Source = source;
            Target = target;
            Controller = controller;
            Condition = condition;
        }

        public void Execute()
        {
            Controller.Run();
        }

        public bool CanExecute()
        {
            return Condition();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}