using System;
using Controller.Implementation;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Commands.Implementation
{
    public class DeleteCommandBase : CRUDCommandBase
    {
        public DeleteCommandBase(IDTOWrapper source, IConvertibleCollection target, Func<bool> condition)
            : base(source, target, new DeleteControllerBase(source, target), condition)
        {
        }
    }
}