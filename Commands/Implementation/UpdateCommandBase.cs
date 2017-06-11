using Controller.Implementation;
using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace Commands.Implementation
{
    public class UpdateCommandBase<TDomainClass> : CRUDCommandBase<TDomainClass> 
        where TDomainClass : class, IDomainClass
    {
        public UpdateCommandBase(
            IDomainObjectWrapper<TDomainClass> domainObjectWrapper,
            IHasActionViewState viewStateObject,
            IInMemoryCollection<TDomainClass> collection) 
            : base(domainObjectWrapper, viewStateObject, collection)
        {
            Controller = new UpdateControllerBase<TDomainClass>(domainObjectWrapper, collection);
        }

        public override bool CanExecute()
        {
            return (DomainObjectWrapper.DomainObject != null) && (ViewStateObject.ActionViewState == ViewActionStateType.Update);
        }
    }
}