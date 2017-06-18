using ViewActionState.Types;

namespace ViewActionState.Interfaces
{
    public interface IHasViewActionState
    {
        ViewActionStateType ViewActionState { get; }
    }
}