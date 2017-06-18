using ExtensionsModel.Interfaces;
using InMemoryStorage.Implementation;

namespace ExtensionsModel.Implementation
{
    public abstract class DomainClassWithImage : StorableBase, IClassWithImage
    {
        private int _imageKey;

        public int ImageKey
        {
            get { return _imageKey; }
            set { _imageKey = value; }
        }

        protected DomainClassWithImage(int key, int imageKey) : base(key)
        {
            ImageKey = imageKey;
        }
    }
}