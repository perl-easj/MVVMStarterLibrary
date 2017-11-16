using InMemoryStorage.Interfaces;
using Persistency.Implementation;

namespace WebAPI.Implementation
{
    /// <summary>
    /// Since a WebAPI data source does not support the Save operation,
    /// that source is configured with a "Not Supported" strategy object.
    /// </summary>
    public class ConfiguredWebAPISource<TDTO> : ConfiguredPersistentSource<TDTO> where TDTO : IStorable
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