using DataTransformation.Implementation;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// Further specialisation of the FilePersistableCatalog class,
    /// for scenarios where no DTO-transformation is needed.
    /// the FilePersistableCatalog class:
    /// </summary>
    public class FilePersistableCatalogNoDTO<T, TVMO> : FilePersistableCatalog<T, TVMO, T>
        where T : class, ITransformed<T>, new() 
        where TVMO : IStorable
    {
        public FilePersistableCatalogNoDTO(IFactory<T, TVMO> vmFactory)
            : base(vmFactory, new IdenticalDataFactory<T>())
        {
        }
    }
}