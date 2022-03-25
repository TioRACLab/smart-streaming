//https://plbonneville.com/blog/listing-zip-file-content-in-blazor/

using System.IO.Compression;
using SmartStream.Client.Helpers;
using SmartStream.Client.Services.Zip;

namespace SmartStream.Client.Services
{
    public class ZipService
    {
        public async Task<List<ZipEntry>> ExtractFiles(Stream fileData)
        {
            await using var ms = new MemoryStream();
            await fileData.CopyToAsync(ms);

            using var archive = new ZipArchive(ms);

            var entries = new List<ZipEntry>();

            foreach (var entry in archive.Entries)
            {
                await using var fileStream = entry.Open();
                var fileBytes = await fileStream.ReadFully();

                entries.Add(new ZipEntry { Name = entry.FullName, Content = fileBytes });
            }

            return entries;
        }
    }
}
