using System;
using Filtering.Interfaces;

namespace Filtering.Implementation
{
    /// <summary>
    /// Implementation of IFilter interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object to which the filter is applied.
    /// </typeparam>
    public class Filter<T> : IFilter<T>
    {
        #region Instance fields
        private bool _on;
        private string _id;
        private Func<T, bool> _filter;
        #endregion

        #region Constructor
        public Filter(string id, Func<T, bool> filter)
        {
            _id = id;
            _filter = filter;
            _on = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Identifier for the Filter
        /// </summary>
        public string ID
        {
            get { return _id; }
        }

        /// <summary>
        /// A "switch" by which the filter can be turned on or off.
        /// If a filter is "off", it will always return true.
        /// </summary>
        public bool On
        {
            get { return _on; }
            set { _on = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Toggles the on/off state of the filter
        /// </summary>
        public void Toggle()
        {
            _on = !_on;
        }

        /// <summary>
        /// The method acting as the actual filter: 
        /// returns true if the given object "passes" 
        /// the filter, otherwise false.
        /// </summary>
        /// <param name="obj">
        /// Object to which the filter condition is applied.
        /// </param>
        public bool Condition(T obj)
        {
            return !_on || _filter(obj);
        } 
        #endregion
    }
}