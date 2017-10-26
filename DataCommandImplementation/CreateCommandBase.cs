using System;
using DataController.Implementation;
using DataTransformation.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Create command.
    /// </summary>
    public class CreateCommandBase : CRUDCommandBase
    {
        public CreateCommandBase(ITransformedDataWrapper source, ITransformedDataCollection target, Func<bool> condition)
            : base(source, target, new CreateControllerBase(source, target), condition)
        {
        }
    }
}