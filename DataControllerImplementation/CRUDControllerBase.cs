using Catalog.Interfaces;
using Controller.Interfaces;
using DataTransformation.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Base class for controllers performing CRUD 
    /// (Create, Read, Update, Delete) operations.
    /// It is assumed that the controllers operate
    /// on transformed data objects, obtain the source 
    /// object from a transformed data object wrapper, 
    /// and perform the operation itself on implementation 
    /// of ITransformedDataCollection.
    /// </summary>
    public abstract class CRUDControllerBase<TVMO> : ISimpleController 
    {
        protected IDataWrapper<TVMO> Source;
        protected ICatalog<TVMO> Target;

        protected CRUDControllerBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target)
        {
            Source = source;
            Target = target;
        }

        public abstract void Run();
    }
}