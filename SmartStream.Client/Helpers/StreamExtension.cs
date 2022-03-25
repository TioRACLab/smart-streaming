namespace SmartStream.Client.Helpers
{
    public static class StreamExtension
    {
        //https://plbonneville.com/blog/listing-zip-file-content-in-blazor/
        public static async Task<byte[]> ReadFully(this Stream input)
        {
            await using var ms = new MemoryStream();
            await input.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
