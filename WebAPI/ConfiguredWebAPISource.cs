using Persistency.Implementation;

namespace WebAPI.Implementation
{
    public class ConfiguredWebAPISource<TDTO> : ConfiguredPersistentSource<TDTO>
    {
        public ConfiguredWebAPISource(string serverURL, string apiID, string apiPrefix = "api")
        {
            WebAPISource<TDTO> webAPISource = new WebAPISource<TDTO>(serverURL, apiID, apiPrefix);

            DataSourceLoad = webAPISource;
            DataSourceSave = new DataSourceSaveNotSupported<TDTO>();
            DataSourceCRUD = webAPISource;
        }
    }
}