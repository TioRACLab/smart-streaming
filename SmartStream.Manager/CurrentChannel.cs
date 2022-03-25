using SmartStream.Client.Helpers;
using SmartStream.Client.Services.Zip;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartStream.Manager
{
    public class CurrentChannel
    {
        public Channel? Channel { get; private set; }

        public bool Online { get; private set; } = false;

        public List<Content>? Contents { get; private set; }


        public void CreateLocalChannel()
        {
            Channel = new Channel()
            {
                Id = Guid.NewGuid(),
                Name = "New Channel",
                Slug = "new-channel",
                Scenes = new()
            };

            Online = false;
            Contents = new();
        }

        public async Task LoadLocalChannel(Stream channelStream)
        {
            Online = false;
            Contents = new();

            await using var ms = new MemoryStream();
            await channelStream.CopyToAsync(ms);

            using var archive = new ZipArchive(ms);
            var entries = new List<ZipEntry>();

            Channel = await GetDataFromPackage<Channel>(archive, "channel");
            var contents = await GetDataFromPackage<List<Content>>(archive, "content");

            var temp = archive.Entries.Select(e => e.FullName).ToList();

            foreach (var content in contents)
            {
                var fileBytes = await GetStreamFromPackage(archive, $"Contents/{content.Name}");

                Contents.Add(new FileContent
                {
                    Id = content.Id,
                    Name = content.Name,
                    FriendlyName = content.FriendlyName,
                    DataFile = fileBytes
                });
            }
        }

        private async Task<byte[]> GetStreamFromPackage(ZipArchive archive, string name)
        {
            var entry = archive.Entries.FirstOrDefault(e => e.FullName.ToLower() == name.ToLower());
            await using var fileStream = entry.Open();
            return await fileStream.ReadFully();
        }

        private async Task<T> GetDataFromPackage<T>(ZipArchive archive, string name)
        {
            var fileBytes = await GetStreamFromPackage(archive, name);
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(fileBytes));
        }
    }
}
