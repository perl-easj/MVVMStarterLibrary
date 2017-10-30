using Catalog.Interfaces;
using DataTransformation.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Insert operation.
    /// </summary>
    public class CreateControllerBase<TVMO> : CRUDControllerBase<TVMO>
    {
        public CreateControllerBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target)
            : base(source, target)
        {
        }

        /// <summary>
        /// Inserts the retrieved transformed data object
        /// into the target collection.
        /// </summary>
        public override void Run()
        {
            Target.Create(Source.DataObject);
        }
    }
}