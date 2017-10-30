using Catalog.Interfaces;
using DataTransformation.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Update operation.
    /// </summary>
    public class UpdateControllerBase<T, TVMO> : CRUDControllerBase<TVMO>
        where TVMO : class, ITransformed<T>
    {
        public UpdateControllerBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target)
            : base(source, target)
        {
        }

        /// <summary>
        /// Update is performed by deleting the existing 
        /// data object with the corresponding Key, and 
        /// then inserting the updated object.
        /// </summary>
        public override void Run()
        {
            TVMO updateObj = Source.DataObject.Clone() as TVMO;
            Target.Update(updateObj, Source.DataObject.Key);
        }
    }
}