using Controller.Implementation;
using DataClass.Implementation;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace Commands.Implementation
{
    public class UpdateCommandBase<TDTO> : CRUDCommandBase<TDTO>
        where TDTO : DTOBaseWithKey
    {
        public UpdateCommandBase(
            IDTOWrapper<TDTO> objectWrapper,
            IHasActionViewState viewStateObject,
            IConvertibleInMemoryCollection<TDTO> collection) 
            : base(objectWrapper, viewStateObject, collection)
        {
            Controller = new UpdateControllerBase<TDTO>(objectWrapper, collection);
        }

        public override bool CanExecute()
        {
            return (ObjectWrapper.DataObject != null) && (ViewStateObject.ActionViewState == ViewActionStateType.Update);
        }
    }
}