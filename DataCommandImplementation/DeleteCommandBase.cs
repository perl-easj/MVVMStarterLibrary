using System;
using DataController.Implementation;
using DTO.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete command.
    /// </summary>
    public class DeleteCommandBase : CRUDCommandBase
    {
        public DeleteCommandBase(IDTOWrapper source, IDTOCollection target, Func<bool> condition)
            : base(source, target, new DeleteControllerBase(source, target), condition)
        {
        }
    }
}