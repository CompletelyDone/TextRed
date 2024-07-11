namespace Model
{
    public struct TextProcessProps
    {
        public string PathIn { get; set; }
        public string PathOut { get; set; }
        public int MinWordLength { get; set; }
        public bool RemovePunctuation { get; set; }
    }
}
