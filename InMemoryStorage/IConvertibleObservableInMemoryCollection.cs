namespace InMemoryStorage.Interfaces
{
    public interface IConvertibleObservableInMemoryCollection<TDTO> :
        IObservableInMemoryCollection,
        IConvertibleInMemoryCollection<TDTO>
    {      
    }
}