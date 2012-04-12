namespace CommandCentral.CommonClasses
{
    class Product
    {
        public string Name { get; set; }
        public string RateType { get; set; }
        public string Rate { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Overview { get; set; }
        public Category Category { get; set; }
        public string SubCategory { get; set; }
        
        public Product()
        {
            this.Name = string.Empty;
            this.RateType = string.Empty;
            this.Rate = string.Empty;
            this.Width = string.Empty;
            this.Height = string.Empty;
            this.Overview = string.Empty;
            this.SubCategory = string.Empty;
        }
    }
}
