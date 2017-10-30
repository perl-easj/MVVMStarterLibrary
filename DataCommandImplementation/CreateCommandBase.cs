using System;
using Catalog.Interfaces;
using DataController.Implementation;
using DataTransformation.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Create command.
    /// </summary>
    public class CreateCommandBase<TVMO> : CRUDCommandBase
    {
        public CreateCommandBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target, Func<bool> condition)
            : base(new CreateControllerBase<TVMO>(source, target), condition)
        {
        }
    }
}