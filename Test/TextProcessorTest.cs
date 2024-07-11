using Model;

namespace Test
{
    public class TextProcessorTest
    {
        [Fact]
        public void FileCreated()
        {
            var pathIn = "D:\\Sobesi\\TextRed\\Test\\Texts\\LoremIpsum.txt";
            var pathOut = "D:\\Sobesi\\TextRed\\Test\\Texts\\NewLoremIpsum.txt";
            int minWordLength = 4;
            bool removePunctuation = true;

            TextProcessor.ProcessFiles(new[] 
            { 
                new TextProcessProps(pathIn, pathOut, minWordLength, removePunctuation) 
            });

            Assert.True(File.Exists(pathOut));
        }
        [Fact]
        public void FilesCheck()
        {
            var pathIn = "D:\\Sobesi\\TextRed\\Test\\Texts\\LoremIpsum.txt";
            var pathOut = "D:\\Sobesi\\TextRed\\Test\\Texts\\NewLoremIpsum.txt";
            var path2In = "D:\\Sobesi\\TextRed\\Test\\Texts\\VoynaIMir.txt";
            var path2Out = "D:\\Sobesi\\TextRed\\Test\\Texts\\NewVoynaIMir.txt";
            int minWordLength = 4;
            bool removePunctuation = true;

            TextProcessor.ProcessFiles(new[]
            {
                new TextProcessProps(pathIn, pathOut, minWordLength, removePunctuation),
                new TextProcessProps(path2In, path2Out, minWordLength, removePunctuation)
            });

            Assert.True(File.Exists(pathOut));
            Assert.True(File.Exists(path2Out));
        }
    }
}