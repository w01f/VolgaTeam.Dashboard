using System.Collections.Generic;

namespace CommandCentral.CommonClasses
{
    class SearchGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public SearchGroup()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Tags = new List<string>();
        }
    }
}
