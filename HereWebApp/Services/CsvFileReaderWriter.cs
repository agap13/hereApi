using CsvHelper;
using HereWebApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HereWebApp.Services
{
    public class CsvFileReaderWriter
    {
        private readonly string _pathFile = "Points.csv";
        public void WriteToFile(IEnumerable<PointDTO> points)
        {
            using (var streamWriter = new StreamWriter(_pathFile))
            {
                using (var writer = new CsvWriter(streamWriter))
                {
                    writer.WriteRecords(points);
                }
            }
        }

        public async Task<string> ReadAndDeleteFile(CancellationToken ct)
        {
            var text = await ReadFromFile(ct);
            DeleteFile();
            return text;
        }

        private async Task<string> ReadFromFile(CancellationToken ct)
        {
            return await File.ReadAllTextAsync(_pathFile, ct);
        }

        private void DeleteFile()
        {
            if (File.Exists(_pathFile))
            {
                File.Delete(_pathFile);
            }
        }
    }
}
