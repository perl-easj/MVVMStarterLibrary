using Controller.Interfaces;
using DTO.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Base class for controllers performing CRUD 
    /// (Create, Read, Update, Delete) operations.
    /// It is assumed that the controllers operate on DTOs,
    /// obtain the source object from an IDTOWrapper, and
    /// perform the operation itself on an IDTOCOllection.
    /// </summary>
    public abstract class CRUDControllerBase : ISimpleController 
    {
        protected IDTOWrapper Source;
        protected IDTOCollection Target;

        protected CRUDControllerBase(IDTOWrapper source, IDTOCollection target)
        {
            Source = source;
            Target = target;
        }

        public abstract void Run();
    }
}