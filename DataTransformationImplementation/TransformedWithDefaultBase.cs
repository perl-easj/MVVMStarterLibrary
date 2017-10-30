using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public abstract class TransformedWithDefaultBase<T> : TransformedBase<T>, ITransformedWithDefault<T>
    {
        protected TransformedWithDefaultBase()
        {
            SetDefaultValues();
        }

        public abstract void SetDefaultValues();
    }
}