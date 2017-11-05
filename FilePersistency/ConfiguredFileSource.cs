using Persistency.Implementation;
using StringPersistency.Interfaces;

namespace FilePersistency.Implementation
{
    public class ConfiguredFileSource<TDTO> : ConfiguredPersistentSource<TDTO>
    {
        public ConfiguredFileSource(IStringPersistence stringPersistence, IStringConverter<TDTO> stringConverter) 
        {
            FileSource<TDTO> fileSource = new FileSource<TDTO>(stringPersistence, stringConverter);

            DataSourceLoad = fileSource;
            DataSourceSave = fileSource;
            DataSourceCRUD = new DataSourceCRUDNotSupported<TDTO>();
        }
    }
}