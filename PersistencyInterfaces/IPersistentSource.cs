using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistency.Interfaces
{
    public interface IPersistentSource<T>
    {
        void Save(List<T> objects);
        Task<List<T>> Load();
    }
}