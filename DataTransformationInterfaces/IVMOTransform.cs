namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for transformation between a domain object
    /// and a VMO (and vice versa)
    /// </summary>
    public interface IVMOTransform<T, TVMO>
    {
        T CreateDomainObjectFromVMO(TVMO vmObj);
        TVMO CreateVMO(T obj);
    }
}