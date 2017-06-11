using ModelClass.Interfaces;

namespace ModelClass.Implementation
{
    /// <summary>
    /// Base class for domain classes.
    /// </summary>
    public abstract class DomainClassBase : IDomainClass
    {
        public const int NullKey = -1;

        /// <summary>
        /// Key property. NB: Keys are handled by the model base classes,
        /// and any key value set externally will be overwritten.
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Ensures that default values are set when a domain
        /// object is created.
        /// </summary>
        protected DomainClassBase()
        {
            SetDefaultValues();
        }

        public IDomainClass Clone()
        {
            return (IDomainClass)MemberwiseClone();
        }

        public abstract void SetDefaultValues();
    }
}