using DTO.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class is intended for scenarios where there is no need
    /// for the DTO "layer" in the architecture. That is, the domain
    /// objects are also acting as DTOs, and thus implement the IDTO
    /// interface.
    /// </summary>
    public class NoDTOCatalog : FilePersistableCatalog<IDTO>
    {
        /// <summary>
        /// Since FilePersistableCatalog requires a DTO factory, a "trivial"
        /// factory is defined, which just returns the given object unaltered.
        /// </summary>
        private class TrivialDTOFactory : IDTOFactory<IDTO>
        {
            public IDTO Create(IDTO obj)
            {
                return obj;
            }
        }

        protected NoDTOCatalog() : base(new TrivialDTOFactory())
        {
        }

        public override IDTO ConvertDTO(IDTO obj)
        {
            return obj;
        }
    }
}