using ModelClass.Interfaces;

namespace ViewModel.Implementation
{
    public class DomainObjectWrapper<TDomainClass> : IDomainObjectWrapper<TDomainClass>
    {
        private TDomainClass _domainObject;

        public TDomainClass DomainObject
        {
            get { return _domainObject; }
            private set { _domainObject = value; }
        }

        protected DomainObjectWrapper(TDomainClass obj)
        {
            DomainObject = obj;
        }
    }
}