using DTO.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete operation.
    /// </summary>
    public class DeleteControllerBase : CRUDControllerBase
    {
        public DeleteControllerBase(IDTOWrapper source, IDTOCollection target)
            : base(source, target)
        {
        }

        public override void Run()
        {
            Target.DeleteDTO(Source.DataObject.Key);
        }
    }
}