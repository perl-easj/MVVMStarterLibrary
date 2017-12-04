﻿using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;

namespace DataTransformation.Implementation
{
    /// <summary>
    /// Base implementation of IDefaultValues.
    /// Simply calls SetDefaultValues on constructor.
    /// </summary>
    public abstract class CopyableWithDefaultValuesBase : CopyableBase, IDefaultValues
    {
        protected CopyableWithDefaultValuesBase() : base(NullKey)
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Must be implemented in domain-specific classes
        /// </summary>
        public abstract void SetDefaultValues();
    }
}