using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModelClass.Interfaces;

namespace ViewModel.Implementation
{
    public class DetailsViewModelBase<TDomainClass> : 
        DomainObjectWrapper<TDomainClass>,
        INotifyPropertyChanged
        where TDomainClass : class, IDomainClass
    {

        public DetailsViewModelBase(TDomainClass obj) : base(obj)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}