using DTO.Interfaces;
using ExtensionsServices.Implementation;
using Images.Implementation;
using Images.Interfaces;
using Images.Types;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class ItemViewModelWithImage<TDTO> : ItemViewModelBase<TDTO> 
        where TDTO : class
    {
        private IImage _notFoundImage;

        protected ItemViewModelWithImage(IDTO obj) : base(obj)
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