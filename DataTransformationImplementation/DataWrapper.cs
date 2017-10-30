using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public class DataWrapper<TTDO> : IDataWrapper<TTDO>
    {
        /// <summary>
        /// Returns the wrapped transformed data object
        /// </summary>
        public TTDO DataObject { get; }

        /// <summary>
        /// Constructor. Sets the wrapped transformed data object
        /// </summary>
        protected DataWrapper(TTDO obj)
        {
            DataObject = obj;
        }
    }
}