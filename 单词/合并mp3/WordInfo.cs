namespace 合并mp3
{
    internal class WordInfo
    {
        public string Word { get; set; }
        public string AudioUrl { get; set; }

        public WordInfo(string word, string audioUrl)
        {
            Word = word;
            AudioUrl = audioUrl;
        }
    }
}
