using System.Collections.Generic;
using System.Linq;
using Images.Interfaces;

namespace Images.Implementation
{
    /// <summary>
    /// Implements the ITaggedImageCollection interface, by 
    /// extending the ImageCollection implementation
    /// </summary>
    public class TaggedImageCollection : ImageCollection, ITaggedImageCollection
    {
        /// <summary>
        /// Retrieves all image objects tagged with the given tag.
        /// </summary>
        /// <param name="tag">
        /// Tag used for selecting image objects.
        /// </param>
        /// <returns>
        /// List of image objects tagged with the given tag.
        /// </returns>
        public List<IImage> AllWithTag(string tag)
        {
            List<IImage> filteredImages = new List<IImage>();
            foreach (IImage obj in All)
            {
                ITaggedImage tObj = obj as ITaggedImage;
                if (tObj != null && tObj.ContainsTag(tag))
                {
                    filteredImages.Add(obj);
                }
            }

            return filteredImages;
        }

        /// <summary>
        /// Returns the union of all tags for all image objects.
        /// </summary>
        public List<string> AllTags
        {
            get
            {
                List<string> allTags = new List<string>();
                foreach (IImage obj in All)
                {
                    ITaggedImage tObj = obj as ITaggedImage;
                    if (tObj != null)
                    {
                        allTags.AddRange(tObj.Tags);
                    }
                }

                return allTags.Distinct().ToList();
            }
        }
    }
}