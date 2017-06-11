using Controller.Interfaces;
using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;

namespace Controller.Implementation
{
    public abstract class CRUDControllerBase<TDomainClass> : ICRUDController 
        where TDomainClass : IStorable
    {
        protected IDomainObjectWrapper<TDomainClass> DomainObjectWrapper;
        protected IInMemoryCollection<TDomainClass> Collection;

        protected CRUDControllerBase(IDomainObjectWrapper<TDomainClass> domainObjectWrapper, IInMemoryCollection<TDomainClass> collection)
        {
            DomainObjectWrapper = domainObjectWrapper;
            Collection = collection;
        }

        public abstract void Run();
    }
}