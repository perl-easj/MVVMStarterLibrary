using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataTransformation.Implementation;
using DataTransformation.Interfaces;

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
    public class DetailsViewModelBase<TDO> :
        TypedTransformedDataWrapper<TDO>,
        INotifyPropertyChanged 
        where TDO : class
    {
        public DetailsViewModelBase(ITransformedData obj) : base(obj)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}