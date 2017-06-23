using System.Collections.Generic;
using Newtonsoft.Json;
using Persistency.Interfaces;

namespace FilePersistency.Implementation
{
    public class JSONConverter<T> : IDataConverter<T>
    {
        public string ConvertToString(List<T> objects)
        {
            return JsonConvert.SerializeObject(objects); ;
        }

        public List<T> ConvertFromString(string data)
        {
            return (data == null ? new List<T>() : (List<T>)JsonConvert.DeserializeObject(data, typeof(List<T>)));
        }
    }
}