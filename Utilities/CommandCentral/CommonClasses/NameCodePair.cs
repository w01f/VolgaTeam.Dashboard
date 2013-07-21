namespace CommandCentral.CommonClasses
{
    class NameCodePair
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Index { get; set; }

        public NameCodePair()
        {
            this.Name = string.Empty;
            this.Code = string.Empty;
        }
    }
}
