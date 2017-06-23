using System.Threading.Tasks;

namespace Persistency.Interfaces
{
    public interface IStringPersistence
    {
        Task<string> LoadAsync(string source);
        void SaveAsync(string source, string data);
    }
}