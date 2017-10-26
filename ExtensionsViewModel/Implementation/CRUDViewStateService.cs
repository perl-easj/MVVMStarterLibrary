using System.Collections.Generic;
using ExtensionsCommands.Types;
using ViewState.Implementation;

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class provides a specialisation 
    /// of the ViewState service to four states 
    /// corresponding to CRUD operations.
    /// </summary>
    public class CRUDViewStateService : ViewStateService
    {
        public CRUDViewStateService() : base(new List<string>
        {
            CRUDStates.CreateState,
            CRUDStates.ReadState,
            CRUDStates.UpdateState,
            CRUDStates.DeleteState
        })
        {
        }
    }
}