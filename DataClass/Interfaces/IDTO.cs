using System;

namespace DataClass.Interfaces
{
    public interface IDTO
    {
        /// <summary>
        /// Should provides default values for all instance fields. 
        /// This is needed since data objects must have a 
        /// parameterless default constructor.
        /// </summary>
        void SetDefaultValues();

        /// <summary>
        /// Should set instance fields based on values from provided object.
        /// </summary>
        void SetValuesFromObject(Object obj);

        IDTO Clone();
    }
}