using System;
using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;

namespace DataTransformation.Implementation
{
    /// <summary>
    /// Implementation of IDTO interface, using StorableBase implementation
    /// </summary>
    public abstract class TransformedDataBase : StorableBase, ITransformedData
    {
        /// <summary>
        /// Constructor. Actual implementation of SetDefaultValues is
        /// deferred to sub-classes.
        /// </summary>
        protected TransformedDataBase()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Creates a bit-wise identical copy of the DTO, i.e. a shallow copy.
        /// </summary>
        /// <returns>
        /// Shallow clone of object.
        /// </returns>
        public ITransformedData Clone()
        {
            return (MemberwiseClone() as ITransformedData);
        }

        #region Abstract methods to be implemented in sub-classes

        public abstract void SetDefaultValues();
        public abstract void SetValuesFromObject(Object obj);

        #endregion
    }
}
