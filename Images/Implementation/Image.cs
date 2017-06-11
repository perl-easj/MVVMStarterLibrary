using System.Collections.Generic;
using Images.Interfaces;

namespace Images.Implementation
{
    public class Image : ITaggedImage
    {
        private string _description;
        private string _source;
        private List<string> _tags;

        public Image(string description, string source)
        {
            _description = description;
            _source = source;
            _tags = new List<string>();
        }

        public int Key { get; set; }

        public string Source
        {
            get { return _source; }
        }

        public string Description
        {
            get { return _description; }
        }

        public List<string> Tags
        {
            get { return _tags; }
        }

        public void AddTag(string tag)
        {
            _tags.Add(tag);
        }

        public bool ContainsTag(string tag)
        {
            return _tags.Contains(tag);
        }

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
    }
}