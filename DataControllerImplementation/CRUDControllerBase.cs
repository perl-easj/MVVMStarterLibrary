using Controller.Interfaces;
using DataTransformation.Interfaces;

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
        protected ITransformedDataWrapper Source;
        protected ITransformedDataCollection Target;

        protected CRUDControllerBase(ITransformedDataWrapper source, ITransformedDataCollection target)
        {
            Source = source;
            Target = target;
        }

        public abstract void Run();
    }
}