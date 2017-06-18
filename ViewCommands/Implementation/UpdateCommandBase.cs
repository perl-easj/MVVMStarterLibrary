using System;
using Controller.Implementation;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Commands.Implementation
{
    public class UpdateCommandBase : CRUDCommandBase
    {
        public UpdateCommandBase(IDTOWrapper source, IConvertibleCollection target, Func<bool> condition)
            : base(source, target, new UpdateControllerBase(source, target), condition)
        {
        }
    }
}