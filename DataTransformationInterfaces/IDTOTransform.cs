namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for transformation between a domain object
    /// and a DTO (and vice versa)
    /// </summary>
    public interface IDTOTransform<T, TDTO>
    {
        T CreateDomainObjectFromDTO(TDTO dtoObj);
        TDTO CreateDTO(T obj);
    }
}