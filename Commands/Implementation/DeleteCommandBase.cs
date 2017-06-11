using Controller.Implementation;
using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace Commands.Implementation
{
    public class DeleteCommandBase<TDomainClass> : CRUDCommandBase<TDomainClass>
        where TDomainClass : IStorable
    {
        public DeleteCommandBase(
            IDomainObjectWrapper<TDomainClass> domainObjectWrapper, 
            IHasActionViewState viewStateObject, 
            IInMemoryCollection<TDomainClass> collection) 
            : base(domainObjectWrapper, viewStateObject, collection)
        {
            Controller = new DeleteControllerBase<TDomainClass>(domainObjectWrapper, collection);
        }

        public override bool CanExecute()
        {
            return (DomainObjectWrapper.DomainObject != null) && (ViewStateObject.ActionViewState == ViewActionStateType.Delete);
        }
    }
}