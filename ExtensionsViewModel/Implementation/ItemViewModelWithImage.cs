using ExtensionsServices.Implementation;
using Images.Implementation;
using Images.Interfaces;
using Images.Types;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class ItemViewModelWithImage<TDomainClass> : ItemViewModelBase<TDomainClass>
    {
        private IImage _notFoundImage;

        protected ItemViewModelWithImage(TDomainClass obj) : base(obj)
        {
            _notFoundImage = new Image("Image not found", ServiceProvider.Images.GetAppImageSource(AppImageType.NotFound));
        }

        public override string ImageSource
        {
            get { return ServiceProvider.Images.Read(ImageKey, _notFoundImage).Source; }
        }

        public abstract int ImageKey { get; }
    }
}