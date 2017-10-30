using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataTransformation.Implementation;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for Details ViewModel classes. 
    /// Essentially just implements INotifyPropertyChanged. 
    /// It is assumed that any Details ViewModel object will 
    /// wrap a transformed data object. The transformed data 
    /// object will be strongly typed, such that type-specific 
    /// properties can be directly accessed for view binding.
    /// </summary>
    public class DetailsViewModelBase<TVMO> :
        DataWrapper<TVMO>,
        INotifyPropertyChanged 
        where TVMO : class
    {
        public DetailsViewModelBase(TVMO obj) : base(obj)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}