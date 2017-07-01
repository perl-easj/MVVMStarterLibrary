using DTO.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Update operation.
    /// </summary>
    public class UpdateControllerBase : CRUDControllerBase
    {
        public UpdateControllerBase(IDTOWrapper source, IDTOCollection target)
            : base(source, target)
        {
        }

        /// <summary>
        /// Update is performed by deleting the existing object with the
        /// corresponding Key, and then inserting the updated object.
        /// </summary>
        public override void Run()
        {
            IDTO updateObj = Source.DataObject.Clone();
            Target.DeleteDTO(Source.DataObject.Key);
            Target.InsertDTO(updateObj, false);
        }
    }
}