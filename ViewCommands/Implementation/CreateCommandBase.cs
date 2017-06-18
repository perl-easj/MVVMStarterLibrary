using System;
using Controller.Implementation;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace Commands.Implementation
{
    public class CreateCommandBase : CRUDCommandBase
    {
        public CreateCommandBase(IDTOWrapper source, IConvertibleCollection target, Func<bool> condition)
            : base(source, target, new CreateControllerBase(source, target), condition)
        {
        }
    }
}