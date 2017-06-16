using DataClass.Implementation;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public class UpdateControllerBase<TDTO> : CRUDControllerBase<TDTO>
        where TDTO : DTOBaseWithKey
    {
        public UpdateControllerBase(IDTOWrapper<TDTO> objectWrapper, IConvertibleInMemoryCollection<TDTO> collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            TDTO updateObj = ObjectWrapper.DataObject.Clone() as TDTO;
            Collection.DeleteDTO(ObjectWrapper.DataObject.Key);
            Collection.InsertDTO(updateObj, false);
        }
    }
}