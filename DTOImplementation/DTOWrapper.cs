using DTO.Interfaces;

namespace DTO.Implementation
{
    public class DTOWrapper : IDTOWrapper
    {
        private IDTO _object;

        public IDTO DataObject
        {
            get { return _object; }
            private set { _object = value; }
        }

        protected DTOWrapper(IDTO obj)
        {
            _object = obj;
        }
    }
}