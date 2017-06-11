namespace ModelClass.Interfaces
{
    public interface IDomainObjectWrapper<out T>
    {
        T DomainObject { get; }
    }
}