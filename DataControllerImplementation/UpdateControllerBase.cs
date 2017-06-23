using DTO.Interfaces;

namespace DataController.Implementation
{
    public class UpdateControllerBase : CRUDControllerBase
    {
        public UpdateControllerBase(IDTOWrapper objectWrapper, IDTOCollection collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            IDTO updateObj = ObjectWrapper.DataObject.Clone();
            Collection.DeleteDTO(ObjectWrapper.DataObject.Key);
            Collection.InsertDTO(updateObj, false);
        }
    }
}