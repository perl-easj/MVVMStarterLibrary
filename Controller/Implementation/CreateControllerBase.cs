using DataClass.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public class CreateControllerBase<TDTO> : CRUDControllerBase<TDTO>
    {
        public CreateControllerBase(IDTOWrapper<TDTO> objectWrapper, IConvertibleInMemoryCollection<TDTO> collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.InsertDTO(ObjectWrapper.DataObject);
        }
    }
}