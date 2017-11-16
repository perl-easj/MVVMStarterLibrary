using System;
using DataTransformation.Interfaces;

namespace ViewModel.Interfaces
{
    public interface IItemSelectionChangedEvent<out TVMO>
    {
        event Action<IDataWrapper<TVMO>> ItemSelectionChanged;
    }
}