using System;
using System.IO;
using Xunit;

namespace HoerbertConverter.Tests
{
    public class AudioProcessorTests
    {
        [Fact]
        public void Start_ShouldConvertAudioFile_WhenGivenMp3()
        {
            var inputPath = Path.Combine(Directory.GetCurrentDirectory(), "Fixtures", "mp3");
            var outputPath = Directory.GetCurrentDirectory();


            var audioProcessor = new AudioProcessor();
            audioProcessor.FileProcessed += (sender, eventArgs) => 
            {
                Assert.Equal(Path.Combine(inputPath, "Jahzzar_-_05_-_Siesta.mp3"), eventArgs.InputFilePath);
                Assert.Equal(Path.Combine(outputPath, "0.WAV"), eventArgs.OutputFilePath);
            };

            audioProcessor.Start(inputPath, outputPath);
        }
    }
}
