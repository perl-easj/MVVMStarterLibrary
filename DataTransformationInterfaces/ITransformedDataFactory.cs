namespace DataTransformation.Interfaces
{
    public interface ITransformedDataFactory<T>
    {
        ITransformedData CreateTransformedObject(T obj);
        T CreateDomainObject(ITransformedData tObj);
    }
}