using System.Collections.ObjectModel;
using ExtensionsServices.Implementation;
using Images.Interfaces;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class DetailsViewModelWithSelectableImage<T> : DetailsViewModelBase<T>
    {
        private string _domainTypeName;

        public ObservableCollection<IImage> ImageCollection
        {
            get { return ServiceProvider.Images.GetObservableImageCollection(_domainTypeName); }
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

        protected DetailsViewModelWithSelectableImage(T obj, string domainTypeName)
            : base(obj)
        {
            _domainTypeName = domainTypeName;
        }
    }
}