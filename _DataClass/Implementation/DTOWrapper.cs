using DataClass.Interfaces;

namespace DataClass.Implementation
{
    public class DTOWrapper<T> : IDTOWrapper<T>
    {
        private T _object;

        public T DataObject
        {
            get { return _object; }
            private set { _object = value; }
        }

        protected DTOWrapper(T obj)
        {
            _object = obj;
        }
    }
}