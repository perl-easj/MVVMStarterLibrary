using Controller.Interfaces;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public abstract class CRUDControllerBase : ICRUDController 
    {
        protected IDTOWrapper ObjectWrapper;
        protected IConvertibleCollection Collection;

        protected CRUDControllerBase(IDTOWrapper objectWrapper, IConvertibleCollection collection)
        {
            ObjectWrapper = objectWrapper;
            Collection = collection;
        }

        public abstract void Run();
    }
}