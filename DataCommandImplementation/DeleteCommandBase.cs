using System;
using DataController.Implementation;
using DTO.Interfaces;

namespace DataCommand.Implementation
{
    public class DeleteCommandBase : CRUDCommandBase
    {
        public DeleteCommandBase(IDTOWrapper source, IDTOCollection target, Func<bool> condition)
            : base(source, target, new DeleteControllerBase(source, target), condition)
        {
        }
    }
}