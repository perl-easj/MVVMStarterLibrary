using System;

namespace Persistency.Interfaces
{
    /// <summary>
    /// Interface for a persistency manager. Any client that wises to be
    /// managed w.r.t. when to load/save data can register itself at the
    /// provided delegates. 
    /// </summary>
    public interface IPersistencyManager
    {
        /// <summary>
        /// A client can register its Load method at this delegate.
        /// </summary>
        event Action LoadDelegate;

        /// <summary>
        /// A client can register its Save method at this delegate.
        /// </summary>
        event Action SaveDelegate;

        /// <summary>
        /// Invokes the Load method on all registered clients.
        /// </summary>
        void LoadAll();

        /// <summary>
        /// Invokes the Save method on all registered clients.
        /// </summary>
        void SaveAll();
    }
}