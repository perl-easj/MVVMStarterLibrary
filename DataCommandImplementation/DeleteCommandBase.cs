using System;
using DataController.Implementation;
using DataTransformation.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete command.
    /// </summary>
    public class DeleteCommandBase : CRUDCommandBase
    {
        public DeleteCommandBase(ITransformedDataWrapper source, ITransformedDataCollection target, Func<bool> condition)
            : base(source, target, new DeleteControllerBase(source, target), condition)
        {
        }
    }
}