﻿using Catalog.Interfaces;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;

namespace DataController.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete operation.
    /// </summary>
    public class DeleteControllerBase<TVMO> : CRUDControllerBase<TVMO>
        where TVMO : class, IStorable
    {
        public DeleteControllerBase(IDataWrapper<TVMO> source, ICatalog<TVMO> target)
            : base(source, target)
        {
        }

        /// <summary>
        /// Deletes an object from the target collection,
        /// which matches the key of the source data object.
        /// </summary>
        public override void Run()
        {
            Target.Delete(Source.DataObject.Key);
        }
    }
}