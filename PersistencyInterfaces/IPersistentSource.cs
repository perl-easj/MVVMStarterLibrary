namespace Persistency.Interfaces
{
    /// <summary>
    /// Just a convenient interface containing a complete
    /// set of methods for CRUD and Load/Save
    /// </summary>
    public interface IPersistentSource<TDTO> : IDataSourceCRUD<TDTO>, IDataSourceLoad<TDTO>, IDataSourceSave<TDTO>
    {
    }
}