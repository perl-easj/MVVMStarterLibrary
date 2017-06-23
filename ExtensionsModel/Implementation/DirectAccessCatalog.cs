using System;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace ExtensionsModel.Implementation
{
    public class DirectAccessCatalog<T> : FilePersistableCatalog<T>
        where T : class, IStorable, IDTO
    {
        private class TrivialDTOFactory<U> : IDTOFactory<U>
            where U : IDTO
        {
            public IDTO Create(U obj)
            {
                return obj;
            }
        }

        protected DirectAccessCatalog() : base(new TrivialDTOFactory<T>())
        {
        }

        public override void InsertDTO(IDTO obj, bool replaceKey = true)
        {
            T tObj = obj as T;
            if (tObj == null)
            {
                throw new ArgumentException();
            }

            Insert(tObj, replaceKey);
        }
    }
}