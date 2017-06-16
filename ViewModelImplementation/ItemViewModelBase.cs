using System;
using Windows.UI.Xaml;
using DataClass.Implementation;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    public abstract class ItemViewModelBase<TDataClass> : 
        DTOWrapper<TDataClass>, 
        IItemViewModelDescription,
        IItemViewModelImage
    {
        #region Properties (override in model-specific item view model)
        /// <summary>
        /// Override this property to provide a string description of a domain object. 
        /// This description is then displayed for each item in the Master part of the view.
        /// </summary>
        public virtual string Description
        {
            get { throw new NotImplementedException("If you bind to Description in your Item Template (or otherwise), " +
                                                    "you should override the Description property in your ItemViewModel class."); }
        }

        /// <summary>
        /// Override this property to provide an image source for a domain object. 
        /// This image is then displayed for each item in the Master part of the view.
        /// </summary>
        public virtual string ImageSource
        {
            get
            {
                throw new NotImplementedException("If you bind to ImageSource in your Item Template (or otherwise), " +
                                                  "you should override the ImageSource property in your ItemViewModel class.");
            }
        }

        /// <summary>
        /// Override this property to provide a font size for the description. 
        /// </summary>
        public virtual int FontSize
        {
            get { return 18; }
        }

        /// <summary>
        /// Override this property to define the visibility of the image part
        /// </summary>
        public virtual Visibility ImageVisibility
        {
            get { return Visibility.Visible; }
        }

        /// <summary>
        /// Override this property to define the size of the image part
        /// </summary>
        public virtual int ImageSize
        {
            get { return 80; }
        }
        #endregion

        protected ItemViewModelBase(TDataClass obj) : base(obj)
        {
        }
    }
}