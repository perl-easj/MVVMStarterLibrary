using DTO.Interfaces;

namespace DataController.Implementation
{
    public class CreateControllerBase : CRUDControllerBase
    {
        public CreateControllerBase(IDTOWrapper objectWrapper, IDTOCollection collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.InsertDTO(ObjectWrapper.DataObject);
        }
    }
}