using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;

namespace DataTransformation.Implementation
{
    public abstract class TransformedBase<T> : StorableBase, ITransformed<T>
    {
        protected TransformedBase() : base(NullKey)
        {    
        }

        protected TransformedBase(int key) : base(key)
        {            
        }

        protected TransformedBase(T obj)
        {
            SetValuesFromObject(obj);
        }

        public ITransformed<T> Clone()
        {
            return (MemberwiseClone() as ITransformed<T>);
        }

        public abstract void SetValuesFromObject(T obj);
    }
}