namespace DTO.Interfaces
{
    /// <summary>
    /// Interface for classes capable of producing a DTO (Data Transfer Object),
    /// from a given object. This could e.g. be a domain class.
    /// </summary>
    public interface IDTOFactory<in T>
    {
        /// <summary>
        /// Create DTO based on the given object
        /// </summary>
        /// <param name="obj">
        /// Object acting a source for the DTO
        /// </param>
        /// <returns>
        /// Reference to produced DTO.
        /// </returns>
        IDTO Create(T obj);
    }
}