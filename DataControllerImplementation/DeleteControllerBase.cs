using DTO.Interfaces;

namespace DataController.Implementation
{
    public class DeleteControllerBase : CRUDControllerBase
    {
        public DeleteControllerBase(IDTOWrapper objectWrapper, IDTOCollection collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.DeleteDTO(ObjectWrapper.DataObject.Key);
        }
    }
}