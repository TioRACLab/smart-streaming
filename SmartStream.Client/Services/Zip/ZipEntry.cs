//https://plbonneville.com/blog/listing-zip-file-content-in-blazor/
using System.Text;

namespace SmartStream.Client.Services.Zip
{
    public record ZipEntry
    {
        public string Name { get; init; }

        public string MimeType { get; init; }

        public byte[] Content { get; init; }

        public string ContentText
        {
            get
            {
                return Encoding.UTF8.GetString(Content);
            }
        }

        public string ContentBase64
        {
            get
            {
                return Convert.ToBase64String(Content);
            }
        }

        public string ContentHtmlBase64
        {
            get
            {
                return $"data:{MimeType};base64,{ContentBase64}";
            }
        }
    }
}
