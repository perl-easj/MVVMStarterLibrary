using Controller.Implementation;
using DataClass.Implementation;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace Commands.Implementation
{
    public class DeleteCommandBase<TDTO> : CRUDCommandBase<TDTO>
        where TDTO : DTOBaseWithKey
    {
        public DeleteCommandBase(
            IDTOWrapper<TDTO> objectWrapper,
            IHasActionViewState viewStateObject,
            IConvertibleInMemoryCollection<TDTO> collection)
            : base(objectWrapper, viewStateObject, collection)
        {
            Controller = new DeleteControllerBase<TDTO>(objectWrapper, collection);
        }

        public override bool CanExecute()
        {
            return (ObjectWrapper.DataObject != null) && (ViewStateObject.ActionViewState == ViewActionStateType.Delete);
        }
    }
}