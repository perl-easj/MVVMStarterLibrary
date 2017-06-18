using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public class CreateControllerBase : CRUDControllerBase
    {
        public CreateControllerBase(IDTOWrapper objectWrapper, IConvertibleCollection collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.InsertDTO(ObjectWrapper.DataObject);
        }
    }
}