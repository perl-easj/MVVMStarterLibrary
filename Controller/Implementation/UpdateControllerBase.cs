using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public class UpdateControllerBase : CRUDControllerBase
    {
        public UpdateControllerBase(IDTOWrapper objectWrapper, IConvertibleCollection collection)
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