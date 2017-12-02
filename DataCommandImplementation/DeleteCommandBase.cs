using System;
using Catalog.Interfaces;
using DataController.Implementation;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete command.
    /// </summary>
    public class DeleteCommandBase<TVMO> : CRUDCommandBase
        where TVMO : class, ICopyable, IStorable
    {
        public DeleteCommandBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target, Func<bool> condition)
            : base(new DeleteControllerBase<TVMO>(source, target), condition)
        {
        }
    }
}