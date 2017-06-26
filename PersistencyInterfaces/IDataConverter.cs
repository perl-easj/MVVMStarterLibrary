using System.Collections.Generic;

namespace Persistency.Interfaces
{
    public interface IDataConverter<T>
    {
        string ConvertToString(List<T> objects);
        List<T> ConvertFromString(string data);
    }
}