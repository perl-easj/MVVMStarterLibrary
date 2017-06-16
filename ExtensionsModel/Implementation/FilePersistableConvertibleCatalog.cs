using System.Collections.Generic;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;

namespace ExtensionsModel.Implementation
{
    public abstract class FilePersistableConvertibleCatalog<TDTO, TDO> :
        FilePersistableCatalog<TDO>,
        IConvertibleObservableInMemoryCollection<TDTO> 
        where TDO : class, IStorable 
        where TDTO : class, IDTO, new()
    {
        public List<TDTO> AllDTO
        {
            get
            {
                List<TDTO> convertedCollection = new List<TDTO>();
                foreach (TDO obj in All)
                {
                    TDTO cObj = new TDTO();
                    cObj.SetValuesFromObject(obj);
                    convertedCollection.Add(cObj);
                }

                return convertedCollection;
            }
        }

        public TDTO ReadDTO(int key)
        {
            TDO obj = Read(key);
            if (obj == null)
            {
                return null;
            }

            TDTO cObj = new TDTO();
            cObj.SetValuesFromObject(obj);
            return cObj;
        }

        public void DeleteDTO(int key)
        {
            Delete(key);
        }

        public abstract void InsertDTO(TDTO obj, bool replaceKey = true);
    }
}