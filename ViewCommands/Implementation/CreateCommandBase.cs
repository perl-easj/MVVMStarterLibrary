using Controller.Implementation;
using DataClass.Implementation;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace Commands.Implementation
{
    public class CreateCommandBase<TDTO> : CRUDCommandBase<TDTO> 
        where TDTO : DTOBase
    {
        public CreateCommandBase(
            IDTOWrapper<TDTO> objectWrapper,
            IHasActionViewState viewStateObject,
            IConvertibleInMemoryCollection<TDTO> collection) 
            : base(objectWrapper, viewStateObject, collection)
        {
            Controller = new CreateControllerBase<TDTO>(objectWrapper, collection);
        }

        public override bool CanExecute()
        {
            return (ObjectWrapper.DataObject != null) && (ViewStateObject.ActionViewState == ViewActionStateType.Create);
        }
    }
}