using Controller.Interfaces;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public abstract class CRUDControllerBase<TDTO> : ICRUDController 
    {
        protected IDTOWrapper<TDTO> ObjectWrapper;
        protected IConvertibleInMemoryCollection<TDTO> Collection;

        protected CRUDControllerBase(IDTOWrapper<TDTO> objectWrapper, IConvertibleInMemoryCollection<TDTO> collection)
        {
            ObjectWrapper = objectWrapper;
            Collection = collection;
        }

        public abstract void Run();
    }
}