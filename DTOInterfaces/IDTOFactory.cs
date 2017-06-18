namespace DTO.Interfaces
{
    public interface IDTOFactory<in T>
    {
        IDTO Create(T obj);
    }
}