using System;
using DataController.Implementation;
using DTO.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Create command.
    /// </summary>
    public class CreateCommandBase : CRUDCommandBase
    {
        public CreateCommandBase(IDTOWrapper source, IDTOCollection target, Func<bool> condition)
            : base(source, target, new CreateControllerBase(source, target), condition)
        {
        }
    }
}