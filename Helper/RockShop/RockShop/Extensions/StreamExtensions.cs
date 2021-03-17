using System.IO;
using System.Threading.Tasks;

namespace RockShop.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ToByteArrayAsync(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using MemoryStream ms = new MemoryStream();

            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                await ms.WriteAsync(buffer, 0, read);

            return ms.ToArray();
        }
    }
}
