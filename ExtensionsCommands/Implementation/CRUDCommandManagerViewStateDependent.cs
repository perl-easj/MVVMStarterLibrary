using System;
using DataCommand.Implementation;
using DTO.Interfaces;
using ExtensionsCommands.Types;
using ViewState.Interfaces;

namespace ExtensionsCommands.Implementation
{
    /// <summary>
    /// CRUD Command manager where CanExecute predicates depend on view state.
    /// Needs a reference to an object implementing IHasViewState.
    /// </summary>
    public class CRUDCommandManagerViewStateDependent : CRUDCommandManager
    {
        private IHasViewState _viewStateObject;

        public CRUDCommandManagerViewStateDependent(IDTOWrapper source, IDTOCollection target, IHasViewState viewStateObject) : base(source, target)
        {
            _viewStateObject = viewStateObject ?? throw new ArgumentException(nameof(CRUDCommandManagerViewStateDependent));
        }

        protected override bool FurtherConditionCreate()
        {
            return _viewStateObject.ViewState == CRUDStates.CreateState;
        }

        protected override bool FurtherConditionUpdate()
        {
            return _viewStateObject.ViewState == CRUDStates.UpdateState;
        }

        protected override bool FurtherConditionDelete()
        {
            return _viewStateObject.ViewState == CRUDStates.DeleteState;
        }
    }
}