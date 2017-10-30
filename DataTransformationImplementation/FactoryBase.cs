using System.Collections.Generic;
using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public abstract class FactoryBase<T, TTDO> : IFactory<T, TTDO> 
        where TTDO : class, ITransformed<T>, new()
    {
        public TTDO CreateTransformedObject(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            TTDO dtoObj = new TTDO();
            dtoObj.SetValuesFromObject(obj);
            return dtoObj;
        }

        public abstract T CreateDomainObject(TTDO tObj);

        public List<TTDO> CreateTransformedObjects(List<T> objects)
        {
            List<TTDO> tObjects = new List<TTDO>();
            foreach (T obj in objects)
            {
                tObjects.Add(CreateTransformedObject(obj));
            }
            return tObjects;
        }

        public List<T> CreateDomainObjects(List<TTDO> tObjects)
        {
            List<T> objects = new List<T>();
            foreach (TTDO tObj in tObjects)
            {
                objects.Add(CreateDomainObject(tObj));
            }
            return objects;
        }
    }
}