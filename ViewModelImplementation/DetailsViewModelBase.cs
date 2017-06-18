using System.ComponentModel;
using System.Runtime.CompilerServices;
using DTO.Implementation;
using DTO.Interfaces;

namespace ViewModel.Implementation
{
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