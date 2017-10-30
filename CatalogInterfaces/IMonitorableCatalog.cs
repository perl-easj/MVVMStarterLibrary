using System;

namespace Catalog.Interfaces
{
    /// <summary>
    /// A collection that enables a client to be 
    /// notified about changes in the collection 
    /// should implement this interface. Clients can
    /// then register callbacks at the collection.
    /// </summary>
    public interface IMonitorableCatalog
    {
        /// <summary>
        /// Adds a method to be be invoked when 
        /// a new object is created.
        /// </summary>
        void AddOnObjectCreatedCallback(Action callback);

        /// <summary>
        /// Adds a method to be be invoke when 
        /// an object is updated.
        /// </summary>
        void AddOnObjectUpdatedCallback(Action callback);

        /// <summary>
        /// Adds a method to be be invoke when 
        /// an object is deleted.
        /// </summary>
        void AddOnObjectDeletedCallback(Action callback);
    }
}