using DTO.Interfaces;

namespace ExtensionsModel.Implementation
{
    public class NoDTOCatalog : FilePersistableCatalog<IDTO>
    {
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

        public override void InsertDTO(IDTO obj, bool replaceKey = true)
        {
            Insert(obj, replaceKey);
        }
    }
}