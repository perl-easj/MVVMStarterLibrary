using System;
using DataController.Implementation;
using DataTransformation.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Update command.
    /// </summary>
    public class UpdateCommandBase : CRUDCommandBase
    {
        public UpdateCommandBase(ITransformedDataWrapper source, ITransformedDataCollection target, Func<bool> condition)
            : base(source, target, new UpdateControllerBase(source, target), condition)
        {
        }
    }
}