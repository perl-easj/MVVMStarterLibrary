using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public abstract class TransformedWithDefaultBase<T> : TransformedBase<T>, ITransformedWithDefault<T>
    {
        protected TransformedWithDefaultBase(T obj) : base(obj)
        {  
        }

        protected TransformedWithDefaultBase() : base(NullKey)
        {
            SetDefaultValues();
        }

        public abstract void SetDefaultValues();
    }
}