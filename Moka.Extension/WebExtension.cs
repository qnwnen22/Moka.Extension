using System;
using System.IO;
using System.Net;
using System.Text;

namespace Moka.Extension
{
    public static class WebExtension
    {
        public static void Write(this HttpWebRequest httpWebRequest, byte[] bytes)
        {
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static void Write(this HttpWebRequest httpWebRequest, string text, Encoding encoding = null)
        {
            if (encoding is null) { encoding = Encoding.UTF8; }
            byte[] getBytes = encoding.GetBytes(text);
            httpWebRequest.Write(getBytes);
        }

        public static string ReadToEnd(this HttpWebRequest httpWebRequest, Encoding encoding = null)
        {
            if (encoding is null) { encoding = Encoding.UTF8; }
            using (WebResponse webResponse = httpWebRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(stream, encoding))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        public static T ReadToEnd<T>(this HttpWebRequest httpWebRequest, Encoding encoding = null)
        {
            string readToEnd = httpWebRequest.ReadToEnd(encoding);
            if (string.IsNullOrWhiteSpace(readToEnd)) { return default; }
            T toClass = readToEnd.ToClass<T>();
            return toClass;
        }

        public static string ReadToEnd(this WebException webException, Encoding encoding = null)
        {
            if (encoding is null) { encoding = Encoding.UTF8; }
            using (WebResponse webResponse = webException.Response)
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(stream, encoding))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        public static byte[] GetImageBytes(this string imageUri)
        {
            Uri uri = new Uri(imageUri);
            if (uri.IsFile) { imageUri = "https:" + imageUri; }

            WebRequest webRequest = WebRequest.Create(imageUri);
            HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;

            httpWebRequest.Method = WebRequestMethods.Http.Get;
            using (WebResponse webResponse = httpWebRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
