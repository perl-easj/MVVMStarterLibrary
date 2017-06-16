using DataClass.Implementation;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;

namespace Controller.Implementation
{
    public class DeleteControllerBase<TDTO> : CRUDControllerBase<TDTO>
        where TDTO : DTOBaseWithKey
    {
        public DeleteControllerBase(IDTOWrapper<TDTO> objectWrapper, IConvertibleInMemoryCollection<TDTO> collection)
            : base(objectWrapper, collection)
        {
        }

        public override void Run()
        {
            Collection.DeleteDTO(ObjectWrapper.DataObject.Key);
        }
    }
}