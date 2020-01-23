using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Linq;
using System.IO;

namespace HoerbertConverter
{
    class AudioFileConvertedArgs : EventArgs
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public AudioFileConvertedArgs(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }
    }

    class AudioProcessor
    {
        public event EventHandler<AudioFileConvertedArgs> FileProcessed;

        private const int OutputRate = 32000; // 32kHz
        private const string OutputFileExtension = "WAV";

        public void Start(string inputPath, string outputPath)
        {
            var inputFilesPath = Directory.GetFiles(inputPath).OrderBy(f => f);

            var fileNameNumber = 0;
            foreach (var inputFilePath in inputFilesPath)
            {
                var fileName = Path.GetFileNameWithoutExtension(inputFilePath);
                var outputFilePath = Path.Combine(outputPath, $"{fileNameNumber}.{OutputFileExtension}");

                ProcessAudioFile(inputFilePath, outputFilePath);
                OnFileProcessed(new AudioFileConvertedArgs(inputFilePath, outputFilePath));

                fileNameNumber++;
            }
        }

        private void ProcessAudioFile(string inputFilePath, string outputFilePath)
        {
            using var reader = new AudioFileReader(inputFilePath);

            var mono = new StereoToMonoSampleProvider(reader);
            var outputFormat = new WaveFormat(OutputRate, mono.WaveFormat.Channels);

            using var resampler = new MediaFoundationResampler(mono.ToWaveProvider(), outputFormat);
            WaveFileWriter.CreateWaveFile16(outputFilePath, resampler.ToSampleProvider());
        }

        private void OnFileProcessed(AudioFileConvertedArgs args)
        {
            var handler = FileProcessed;
            handler?.Invoke(this, args);
        }
    }
}
