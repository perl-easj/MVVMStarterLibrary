using InMemoryStorage.Interfaces;

namespace ModelClass.Implementation
{
    /// <summary>
    /// Base class for domain classes.
    /// </summary>
    public abstract class DomainClassBase : IStorable
    {
        public const int NullKey = -1;

        /// <summary>
        /// Key property. NB: Keys are handled by the model base classes,
        /// and any key value set externally will be overwritten.
        /// </summary>
        public int Key { get; set; }

        protected DomainClassBase(int key)
        {
            Key = key;
        }
    }
}