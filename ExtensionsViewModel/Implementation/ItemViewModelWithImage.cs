using DTO.Interfaces;
using ExtensionsServices.Implementation;
using Images.Implementation;
using Images.Interfaces;
using Images.Types;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// Base class for an Item ViewModel class containing a image, 
    /// provided by the Images service. The image is thus identified 
    /// by a numeric key (ImageKey).
    /// </summary>
    /// <typeparam name="TDTO">
    /// Actual type of DTO
    /// </typeparam>
    public abstract class ItemViewModelWithImage<TDTO> : ItemViewModelBase<TDTO> 
        where TDTO : class
    {
        private IImage _notFoundImage;

        protected ItemViewModelWithImage(IDTO obj) : base(obj)
        {
            _notFoundImage = new Image("Image not found", ServiceProvider.Images.GetAppImageSource(AppImageType.NotFound));
        }

        /// <summary>
        /// If no Image with a matching key can be found, a well-defined 
        /// fall-back Image is used instead.
        /// </summary>
        public override string ImageSource
        {
            get { return ServiceProvider.Images.Read(ImageKey, _notFoundImage).Source; }
        }

        /// <summary>
        /// Specific implementation of the Key property is done in sub-classes,
        /// since the specific source for the key value may vary.
        /// </summary>
        public abstract int ImageKey { get; }
    }
}