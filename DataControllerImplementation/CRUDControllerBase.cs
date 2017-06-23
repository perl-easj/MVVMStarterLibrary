using Controller.Interfaces;
using DTO.Interfaces;

namespace DataController.Implementation
{
    public abstract class CRUDControllerBase : ISimpleController 
    {
        protected IDTOWrapper ObjectWrapper;
        protected IDTOCollection Collection;

        protected CRUDControllerBase(IDTOWrapper objectWrapper, IDTOCollection collection)
        {
            ObjectWrapper = objectWrapper;
            Collection = collection;
        }

        public abstract void Run();
    }
}