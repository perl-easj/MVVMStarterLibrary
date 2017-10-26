using DataTransformation.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Insert operation.
    /// </summary>
    public class CreateControllerBase : CRUDControllerBase
    {
        public CreateControllerBase(ITransformedDataWrapper source, ITransformedDataCollection target)
            : base(source, target)
        {
        }

        /// <summary>
        /// Inserts the retrieved transformed data object
        /// into the target collection.
        /// </summary>
        public override void Run()
        {
            Target.InsertTransformed(Source.DataObject);
        }
    }
}