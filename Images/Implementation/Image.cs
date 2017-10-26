using System.Collections.Generic;
using Images.Interfaces;

namespace Images.Implementation
{
    /// <summary>
    /// Implementation of the TaggedImage interface 
    /// (we assume that all images are tagged).
    /// </summary>
    public class TaggedImage : ITaggedImage
    {
        #region Constructor
        public TaggedImage(string description, string source)
        {
            Description = description;
            Source = source;
            Tags = new List<string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Unique identifier for the image.
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Source for the image. This could be a 
        /// path to a file, or a URL.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Description of the image.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// List of tags associated with the image.
        /// </summary>
        public List<string> Tags { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Add a single tag to the set of tags.
        /// </summary>
        /// <param name="tag">
        /// Tag to add.
        /// </param>
        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }

        /// <summary>
        /// Checks if a given tag is part of 
        /// the set of tags.
        /// </summary>
        /// <param name="tag">
        /// Tag to check.
        /// </param>
        /// <returns>
        /// True if given tag is part of the 
        /// set of tags, otherwise false.
        /// </returns>
        public bool ContainsTag(string tag)
        {
            return Tags.Contains(tag);
        }

        /// <summary>
        /// Checks if at least one of the given 
        /// tags is part of the set of tags.
        /// </summary>
        /// <param name="tags">
        /// Tags to check.
        /// </param>
        /// <returns>
        /// True if at least one of the given tags 
        /// is part of the set of tags, otherwise false.
        /// </returns>
        public bool ContainsAnyTag(List<string> tags)
        {
            foreach (string tag in tags)
            {
                if (ContainsTag(tag))
                {
                    return true;
                }
            }

            return false;
        } 
        #endregion
    }
}