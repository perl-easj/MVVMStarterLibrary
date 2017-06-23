using System.Collections.Generic;
using ExtensionsCommands.Types;
using ViewState.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public class CRUDViewStateService : ViewStateService
    {
        public CRUDViewStateService() : base(new List<string> { CRUDStates.CreateState, CRUDStates.ReadState, CRUDStates.UpdateState, CRUDStates.DeleteState })
        {
        }
    }
}