using ViewActionState.Types;

namespace ViewActionState.Interfaces
{
    public interface IHasActionViewState
    {
        ViewActionStateType ActionViewState { get; set; }
    }
}