using System;
using System.Windows.Input;
using Controller.Interfaces;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Commands.Implementation
{
    public abstract class CRUDCommandBase : ICommand 
    {
        protected IDTOWrapper Source;
        protected IConvertibleCollection Target;
        protected ICRUDController Controller;
        protected Func<bool> Condition;

        protected CRUDCommandBase(IDTOWrapper source, IConvertibleCollection target, ICRUDController controller, Func<bool> condition)
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