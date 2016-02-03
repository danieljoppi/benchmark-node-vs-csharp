using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace csharp.Extensions
{
    public static class HttpContentExtension
    {
        public static Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            return content.ReadAsStringAsync().ContinueWith(t =>
                JsonConvert.DeserializeObject<T>(t.Result));
        }
    }
}
