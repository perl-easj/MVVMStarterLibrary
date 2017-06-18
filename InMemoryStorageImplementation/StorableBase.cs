using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
    public abstract class StorableBase : IStorable
    {
        public const int NullKey = -1;

        /// <summary>
        /// Key property. NB: Keys are handled by the model base classes,
        /// and any key value set externally will be overwritten.
        /// </summary>
        public int Key { get; set; }

        protected StorableBase(int key)
        {
            Key = key;
        }
    }
}