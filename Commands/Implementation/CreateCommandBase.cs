using Controller.Implementation;
using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace Commands.Implementation
{
    public class CreateCommandBase<TDomainClass> : CRUDCommandBase<TDomainClass>
        where TDomainClass : class, IDomainClass
    {
        public CreateCommandBase(
            IDomainObjectWrapper<TDomainClass> domainObjectWrapper,
            IHasActionViewState viewStateObject,
            IInMemoryCollection<TDomainClass> collection) 
            : base(domainObjectWrapper, viewStateObject, collection)
        {
            Controller = new CreateControllerBase<TDomainClass>(domainObjectWrapper, collection);
        }

        public override bool CanExecute()
        {
            return (DomainObjectWrapper.DomainObject != null) && (ViewStateObject.ActionViewState == ViewActionStateType.Create);
        }
    }
}