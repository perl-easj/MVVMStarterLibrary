using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;

namespace Controller.Implementation
{
    public class CreateControllerBase<TDomainClass> : CRUDControllerBase<TDomainClass>
        where TDomainClass : class, IDomainClass
    {
        public CreateControllerBase(
            IDomainObjectWrapper<TDomainClass> domainObjectWrapper,
            IInMemoryCollection<TDomainClass> collection)
            : base(domainObjectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.Insert(DomainObjectWrapper.DomainObject.Clone() as TDomainClass);
        }
    }
}