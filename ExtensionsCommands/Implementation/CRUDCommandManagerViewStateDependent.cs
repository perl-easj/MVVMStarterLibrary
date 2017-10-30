using System;
using Catalog.Interfaces;
using DataCommand.Implementation;
using DataTransformation.Interfaces;
using ExtensionsCommands.Types;
using ViewState.Interfaces;

namespace ExtensionsCommands.Implementation
{
    /// <summary>
    /// CRUD Command manager where CanExecute 
    /// predicates depend on view state.
    /// A manager of this type needs a reference 
    /// to an object implementing IHasViewState.
    /// </summary>
    public class CRUDCommandManagerViewStateDependent<T, TVMO> : CRUDCommandManager<T, TVMO> 
        where TVMO : class, ITransformed<T>
    {
        private IHasViewState _viewStateObject;

        public CRUDCommandManagerViewStateDependent(IDataWrapper<TVMO> source, ICatalog<TVMO> target, IHasViewState viewStateObject) : base(source, target)
        {
            _viewStateObject = viewStateObject ?? throw new ArgumentException(nameof(_viewStateObject));
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