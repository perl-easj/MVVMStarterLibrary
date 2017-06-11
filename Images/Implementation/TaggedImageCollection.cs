﻿using System.Collections.Generic;
using System.Linq;
using Images.Interfaces;

namespace Images.Implementation
{
    public class TaggedImageCollection : ImageCollection, ITaggedImageCollection
    {
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