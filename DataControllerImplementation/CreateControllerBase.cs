using DTO.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Insert operation.
    /// </summary>
    public class CreateControllerBase : CRUDControllerBase
    {
        public CreateControllerBase(IDTOWrapper source, IDTOCollection target)
            : base(source, target)
        {
        }

        public override void Run()
        {
            Target.InsertDTO(Source.DataObject);
        }
    }
}