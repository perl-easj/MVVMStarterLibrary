using Filtering.Interfaces;

namespace Filtering.Implementation
{
    public class Filter<T> : IFilter<T>
    {
        public delegate bool FilterCondition(T obj);

        private bool _on;
        private string _id;
        private FilterCondition _filter;

        public Filter(string id, FilterCondition filter)
        {
            _id = id;
            _filter = filter;
            _on = false;
        }

        public string ID
        {
            get { return _id; }
        }

        public bool On
        {
            get { return _on; }
            set { _on = value; }
        }

        public void Toggle()
        {
            _on = !_on;
        }

        public bool Condition(T obj)
        {
            return !_on || _filter(obj);
        }
    }
}