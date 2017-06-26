using DataClass.Interfaces;

namespace DataClass.Implementation
{
    public abstract class DTOBaseWithKey : DTOBase, IDTOWithKey
    {
        public const int NullKey = -1;

        private int _key;

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        protected DTOBaseWithKey()
        {
            _key = NullKey;
        }
    }
}