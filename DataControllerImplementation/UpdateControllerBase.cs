using DataTransformation.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Update operation.
    /// </summary>
    public class UpdateControllerBase : CRUDControllerBase
    {
        public UpdateControllerBase(ITransformedDataWrapper source, ITransformedDataCollection target)
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
            ITransformedData updateObj = Source.DataObject.Clone();
            Target.DeleteTransformed(Source.DataObject.Key);
            Target.InsertTransformed(updateObj, false);
        }
    }
}