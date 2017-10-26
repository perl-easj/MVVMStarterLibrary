using DataTransformation.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete operation.
    /// </summary>
    public class DeleteControllerBase : CRUDControllerBase
    {
        public DeleteControllerBase(ITransformedDataWrapper source, ITransformedDataCollection target)
            : base(source, target)
        {
        }

        /// <summary>
        /// Deletes an object from the target collection,
        /// which matches the key of the source data object.
        /// </summary>
        public override void Run()
        {
            Target.DeleteTransformed(Source.DataObject.Key);
        }
    }
}