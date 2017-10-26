using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public class TransformedDataWrapper : ITransformedDataWrapper
    {
        public ITransformedData DataObject { get; }

        protected TransformedDataWrapper(ITransformedData obj)
        {
            DataObject = obj;
        }
    }
}