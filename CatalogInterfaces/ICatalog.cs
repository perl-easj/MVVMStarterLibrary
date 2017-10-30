using System.Collections.Generic;

namespace Catalog.Interfaces
{
    public interface ICatalog<TVMO>
    {
        /// <summary>
        /// Returns all objects in the collection
        /// </summary>
        List<TVMO> All { get; }

        /// <summary>
        /// Inserts the given object into the collection.
        /// The "replaceKey" parameter controls if the
        /// collection should replace the key with an
        /// internally managed key.
        /// </summary>
        void Create(TVMO obj, bool replaceKey = true);

        /// <summary>
        /// Reads the object in the collection which
        /// matches the given key (if any)
        /// </summary>
        TVMO Read(int key);

        /// <summary>
        /// Inserts the given object into the collection.
        /// The "replaceKey" parameter controls if the
        /// collection should replace the key with an
        /// internally managed key.
        /// </summary>
        void Update(TVMO obj, int key, bool replaceKey = true);

        /// <summary>
        /// Deletes the object matching the key (if any)
        /// from the collection.
        /// </summary>
        void Delete(int key);
    }
}
