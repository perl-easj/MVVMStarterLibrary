using System;
using System.Windows.Input;
using Controller.Interfaces;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;
using ViewActionState.Interfaces;

namespace Commands.Implementation
{
    public abstract class CRUDCommandBase<TDataClass> : ICommand 
    {
        protected IDTOWrapper<TDataClass> ObjectWrapper;
        protected IHasActionViewState ViewStateObject;
        protected IConvertibleInMemoryCollection<TDataClass> Collection;

        protected ICRUDController Controller;

        protected CRUDCommandBase(
            IDTOWrapper<TDataClass> objectWrapper,
            IHasActionViewState viewStateObject,
            IConvertibleInMemoryCollection<TDataClass> collection)
        {
            ObjectWrapper = objectWrapper;
            ViewStateObject = viewStateObject;
            Collection = collection;
        }

        public virtual void Execute()
        {
            Controller.Run();
        }

        public virtual bool CanExecute()
        {
            return true;
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