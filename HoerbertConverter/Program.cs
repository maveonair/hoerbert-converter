using System;
using System.CommandLine;

namespace HoerbertConverter
{
    class Program
    {
        static void Main(string inputPath, string outputPath)
        {
            if (string.IsNullOrEmpty(inputPath))
            {
                Console.WriteLine("Please provide the input directory by using the option --input-path");
                Environment.Exit(-1);
            }

            if (string.IsNullOrEmpty(outputPath))
            {
                Console.WriteLine("Please provide the input directory by using the option --output-path");
                Environment.Exit(-1);
            }

            var audioProcessor = new AudioProcessor();
            audioProcessor.FileProcessed += AudioProcessor_FileProcessed;

            audioProcessor.Start(inputPath, outputPath);
        }

        private static void AudioProcessor_FileProcessed(object sender, AudioFileConvertedArgs args)
        {
            Console.WriteLine($"Converted {args.InputFilePath} to {args.OutputFilePath}");
        }
    }
}
