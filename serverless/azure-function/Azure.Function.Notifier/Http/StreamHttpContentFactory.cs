using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Azure.Function.Notifier.Http
{
    public sealed class StreamHttpContentFactory : IStreamHttpContentFactory
    {
        public HttpContent Create(object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var ms = new MemoryStream();
            SerializeJsonIntoStream(data, ms);
            ms.Seek(0, SeekOrigin.Begin);

            HttpContent httpContent = new StreamContent(ms);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpContent;
        }

        public static void SerializeJsonIntoStream(object data, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, data);
                jtw.Flush();
            }
        }
    }
}
 