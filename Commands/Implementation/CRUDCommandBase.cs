using System;
using System.Windows.Input;
using Controller.Interfaces;
using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;
using ViewActionState.Interfaces;

namespace Commands.Implementation
{
    public abstract class CRUDCommandBase<TDomainClass> : ICommand
        where TDomainClass : IStorable
    {
        protected IDomainObjectWrapper<TDomainClass> DomainObjectWrapper;
        protected IHasActionViewState ViewStateObject;
        protected IInMemoryCollection<TDomainClass> Collection;

        protected ICRUDController Controller;

        protected CRUDCommandBase(
            IDomainObjectWrapper<TDomainClass> domainObjectWrapper,
            IHasActionViewState viewStateObject,
            IInMemoryCollection<TDomainClass> collection)
        {
            DomainObjectWrapper = domainObjectWrapper;
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