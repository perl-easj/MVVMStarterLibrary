using System.ComponentModel;
using System.Runtime.CompilerServices;
using DTO.Implementation;
using DTO.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for Details ViewModel classes. Essentially just implements
    /// INotifyPropertyChanged. It is assumed that any Details ViewModel
    /// object will wrap a DTO. The DTO will be strongly typed, such that
    /// DTO-specific properties can be directly accessed for view binding.
    /// </summary>
    /// <typeparam name="TDTO">
    /// Actual type of DTO.
    /// </typeparam>
    public class DetailsViewModelBase<TDTO> :
        TypedDTOWrapper<TDTO>,
        INotifyPropertyChanged 
        where TDTO : class
    {
        public DetailsViewModelBase(IDTO obj) : base(obj)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}