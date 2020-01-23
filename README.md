# HoerbertConverter

The [Hörbert](https://www.hoerbert.com/) audio player requires a specific WAV format (32khz, 16bit, Mono) and file name (0.WAV, 1.WAV, ...) to play an audio file on this device. This command line utility uses [NAudio](https://github.com/naudio/NAudio) to convert audio files (e.g. mp3, m4a, etc.) to the required audio format.

Attention: NAudio uses specific Windows APIs and therefore this command line utility doesn't work on other platforms such as MacOS or Linux.

## Usage

```cmd
> HoerbertConverter.exe --input-path AudioFilesToConvert --output-path OutputDirectory
```
