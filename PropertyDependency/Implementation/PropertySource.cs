using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyDependency.Interfaces;

namespace PropertyDependency.Implementation
{
    /// <summary>
    /// Contains functionality for classes which are
    /// "property sources", i.e. contains properties 
    /// on which properties/commands in other classes depend. 
    /// </summary>
    public class PropertySource : IPropertySource, INotifyPropertyChanged
    {
        #region Constructor
        public PropertySource()
        {
            PropertyChanged = null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Objects depending on changes to properties 
        /// in this object can call this method to 
        /// register a handler.
        /// </summary>
        public void SetPropertyChangedHandler(PropertyChangedEventHandler handler)
        {
            PropertyChanged += handler;
        } 
        #endregion

        #region OnPropertyChanged code
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}