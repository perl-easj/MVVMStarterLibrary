using System;
using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;

namespace DataTransformation.Implementation
{
    /// <summary>
    /// Base class for classes representing data  
    /// transformed from original domain data
    /// </summary>
    public abstract class TransformedDataBase : StorableBase, ITransformedData
    {
        /// <summary>
        /// Constructor. The specific implementation of 
        /// SetDefaultValues is deferred to sub-classes.
        /// </summary>
        protected TransformedDataBase()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Creates a bit-wise identical copy of the 
        /// transformed data, i.e. a shallow copy.
        /// </summary>
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
