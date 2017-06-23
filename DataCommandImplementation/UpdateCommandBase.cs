using System;
using DataController.Implementation;
using DTO.Interfaces;

namespace DataCommand.Implementation
{
    public class UpdateCommandBase : CRUDCommandBase
    {
        public UpdateCommandBase(IDTOWrapper source, IDTOCollection target, Func<bool> condition)
            : base(source, target, new UpdateControllerBase(source, target), condition)
        {
        }
    }
}