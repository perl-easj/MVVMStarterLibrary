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

        public override void Run()
        {
            Target.DeleteTransformed(Source.DataObject.Key);
        }
    }
}