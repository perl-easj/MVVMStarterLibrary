using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;

namespace Controller.Implementation
{
    public class DeleteControllerBase<TDomainClass> : CRUDControllerBase<TDomainClass>
        where TDomainClass : IStorable
    {
        public DeleteControllerBase(
            IDomainObjectWrapper<TDomainClass> domainObjectWrapper, 
            IInMemoryCollection<TDomainClass> collection) 
            : base(domainObjectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.Delete(DomainObjectWrapper.DomainObject.Key);
        }
    }
}