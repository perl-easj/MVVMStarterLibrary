using System.Collections.ObjectModel;
using ExtensionsServices.Implementation;
using Images.Interfaces;
using ModelClass.Interfaces;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class DetailsViewModelWithSelectableImage<TDomainClass> : DetailsViewModelBase<TDomainClass>
        where TDomainClass : class, IDomainClass
    {
        public ObservableCollection<IImage> ImageCollection
        {
            get { return ServiceProvider.Images.GetObservableImageCollection(typeof(TDomainClass).Name); }
        }

        public IImage ImageSelected
        {
            get { return ServiceProvider.Images.Read(ImageKey); }
            set
            {
                if (value != null)
                {
                    ImageKey = value.Key;
                }

                OnPropertyChanged();
            }
        }

        public abstract int ImageKey { get; set; }

        protected DetailsViewModelWithSelectableImage(TDomainClass obj)
            : base(obj)
        {
        }
    }
}