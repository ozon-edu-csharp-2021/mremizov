using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseApi.Infrastructure.Extensions
{
    public static class HttpExtensions
    {
        public static string ReadHeadersAsString(this IHeaderDictionary headers, string separator = "\n")
        {
            var sb = new StringBuilder();

            foreach (var header in headers)
            {
                sb.Append($"{header.Key}: {header.Value}{separator}");
            }

            return sb.ToString();
        }

        public static async Task<string> ReadBodyAsString(this Stream body)
        {
            body.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
            {
                var bodyText = await reader.ReadToEndAsync();
                body.Seek(0, SeekOrigin.Begin);
                return bodyText;
            }
        }
    }
}
