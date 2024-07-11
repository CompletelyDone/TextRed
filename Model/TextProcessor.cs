using System.Text.RegularExpressions;

namespace Model
{
    public class TextProcessor
    {
        public static void ProcessFiles(IEnumerable<TextProcessProps> props)
        {
            if (props == null) throw new ArgumentNullException(nameof(props));
            Parallel.ForEach(props, properties =>
            {
                ProcessFile(properties.PathIn, properties.PathOut, properties.MinWordLength, properties.RemovePunctuation);
            });
        }

        private static async void ProcessFile(string pathIn, string pathOut, int minWordLength, bool removePunctuation)
        {
            if (!File.Exists(pathIn)) return;
            using (StreamReader sr = new StreamReader(pathIn))
            {
                using (StreamWriter sw = new StreamWriter(pathOut))
                {
                    string? line;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        var processedWords = new List<string>();
                        if (removePunctuation)
                        {
                            line = Regex.Replace(line, @"[^\w\s]", "");
                        }

                        var words = line.Split(' ', StringSplitOptions.TrimEntries);
                        foreach (var word in words)
                        {
                            var clearWord = Regex.Replace(word, @"[^\w\s]", "");
                            if (clearWord.Length >= minWordLength)
                            {
                                processedWords.Add(word);
                            }
                        }
                        await sw.WriteLineAsync(string.Join(" ", processedWords));
                    }

                }
            }
        }
    }
}
