using ExtensionsModel.Interfaces;
using InMemoryStorage.Implementation;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// Base class for any (storable) class 
    /// containing an image.
    /// </summary>
    public abstract class ClassWithImage : StorableBase, IClassWithImage
    {
        public int ImageKey { get; set; }

        protected ClassWithImage(int key, int imageKey) : base(key)
        {
            ImageKey = imageKey;
        }
    }
}