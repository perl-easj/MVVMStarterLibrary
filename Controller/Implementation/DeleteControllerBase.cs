using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public class DeleteControllerBase : CRUDControllerBase
    {
        public DeleteControllerBase(IDTOWrapper objectWrapper, IConvertibleCollection collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.DeleteDTO(ObjectWrapper.DataObject.Key);
        }
    }
}