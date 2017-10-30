using System;
using Catalog.Interfaces;
using DataController.Implementation;
using DataTransformation.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Implementation of a generic Update command.
    /// </summary>
    public class UpdateCommandBase<T, TVMO> : CRUDCommandBase
        where TVMO : class, ITransformed<T>
    {
        public UpdateCommandBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target, Func<bool> condition)
            : base(new UpdateControllerBase<T, TVMO>(source, target), condition)
        {
        }
    }
}