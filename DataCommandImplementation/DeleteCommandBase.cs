using System;
using Catalog.Interfaces;
using DataController.Implementation;
using DataTransformation.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete command.
    /// </summary>
    public class DeleteCommandBase<T, TVMO> : CRUDCommandBase
        where TVMO : class, ITransformed<T>
    {
        public DeleteCommandBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target, Func<bool> condition)
            : base(new DeleteControllerBase<T, TVMO>(source, target), condition)
        {
        }
    }
}