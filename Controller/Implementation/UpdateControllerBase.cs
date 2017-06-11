using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;

namespace Controller.Implementation
{
    public class UpdateControllerBase<TDomainClass> : CRUDControllerBase<TDomainClass>
        where TDomainClass : class, IDomainClass
    {
        public UpdateControllerBase(
            IDomainObjectWrapper<TDomainClass> domainObjectWrapper,
            IInMemoryCollection<TDomainClass> collection)
            : base(domainObjectWrapper, collection)
        {
        }

        public override void Run()
        {
            IDomainClass obj = DomainObjectWrapper.DomainObject.Clone();
            Collection.Delete(obj.Key);
            Collection.Insert(obj as TDomainClass, false);
        }
    }
}