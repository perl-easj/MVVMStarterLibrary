using ExtensionsModel.Interfaces;
using ModelClass.Implementation;

namespace ExtensionsModel.Implementation
{
    public abstract class DomainClassWithImage : DomainClassBase, IClassWithImage
    {
        private int _imageKey;

        public int ImageKey
        {
            get { return _imageKey; }
            set { _imageKey = value; }
        }

        public override void SetDefaultValues()
        {
            ImageKey = NullKey;
        }
    }
}