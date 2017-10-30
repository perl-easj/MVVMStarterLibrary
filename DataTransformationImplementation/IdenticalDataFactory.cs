using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public class IdenticalDataFactory<T> : FactoryBase<T, T> 
        where T : class, ITransformed<T>, new()
    {
        public override T CreateDomainObject(T obj)
        {
            return obj;
        }
    }
}