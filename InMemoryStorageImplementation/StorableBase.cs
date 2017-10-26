using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
    /// <summary>
    /// Implementation of the IStorable interface
    /// </summary>
    public abstract class StorableBase : IStorable
    {
        /// <summary>
        /// Constant indicating that the Key value is not set.
        /// </summary>
        public const int NullKey = -1;

        /// <summary>
        /// Key property. Note that key values set by a client
        /// may be overwritten when the object in inserted into
        /// a colllection, depending on the collection implementation.
        /// </summary>
        public int Key { get; set; }

        #region Constructors
        protected StorableBase() : this(NullKey)
        {
        }

        protected StorableBase(int key)
        {
            Key = key;
        } 
        #endregion
    }
}