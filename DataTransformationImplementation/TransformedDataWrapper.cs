using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    /// <summary>
    /// Implementation of simple wrapper of a 
    /// transformed data object
    /// </summary>
    public class TransformedDataWrapper : ITransformedDataWrapper
    {
        /// <summary>
        /// Returns the wrapped transformed data object
        /// </summary>
        public ITransformedData DataObject { get; }

        /// <summary>
        /// Constructor. Sets the wrapped transformed data object
        /// </summary>
        protected TransformedDataWrapper(ITransformedData obj)
        {
            DataObject = obj;
        }
    }
}