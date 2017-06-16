using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataClass.Implementation;

namespace ViewModel.Implementation
{
    public class DetailsViewModelBase<TDataClass> : 
        DTOWrapper<TDataClass>, 
        INotifyPropertyChanged
    {

        public DetailsViewModelBase(TDataClass obj) : base(obj)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}