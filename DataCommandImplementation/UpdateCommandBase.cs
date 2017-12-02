using System;
using Catalog.Interfaces;
using DataController.Implementation;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Update command.
    /// </summary>
    public class UpdateCommandBase<TVMO> : CRUDCommandBase
        where TVMO : class, ICopyable, IStorable
    {
        public UpdateCommandBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target, Func<bool> condition)
            : base(new UpdateControllerBase<TVMO>(source, target), condition)
        {
        }
    }
}