using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;

namespace DataTransformation.Implementation
{
    public abstract class TransformedBase<T> : StorableBase, ITransformed<T>
    {
        public ITransformed<T> Clone()
        {
            return (MemberwiseClone() as ITransformed<T>);
        }

        public abstract void SetValuesFromObject(T obj);
    }
}