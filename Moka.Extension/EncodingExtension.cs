using System;
using System.Net;
using System.Text;
using System.Web;

namespace Moka.Extension
{
    public static class EncodingExtension
    {
        public static string ToBase64Encode(this string text, Encoding encoding = null, int count = 1)
        {
            if (encoding == null) { encoding = Encoding.UTF8; }
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = encoding.GetBytes(text);
                text = Convert.ToBase64String(bytes);
            }
            return text;
        }

        public static string ToBase64Decode(this string text, Encoding encoding = null, int count = 1)
        {
            if (encoding == null) { encoding = Encoding.UTF8; }
            for (int i = 0; i < count; i++)
            {
                byte[] base64String = Convert.FromBase64String(text);
                text = encoding.GetString(base64String);
            }
            return text;
        }

        public static string ToUrlEncode(this string text, int count = 1)
        {
            string result = text;
            for (int i = 0; i < count; i++)
            {
                result = WebUtility.UrlEncode(result);
            }
            return result;
        }

        public static string ToUrlDecode(this string text, int count = 1)
        {
            string result = text;
            for (int i = 0; i < count; i++)
            {
                result = WebUtility.UrlDecode(result);
            }
            return result;
        }

        public static byte[] GetBytes(this string text, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return encoding.GetBytes(text);
        }

        public static string GetString(this byte[] bytes, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return encoding.GetString(bytes);
        }

        public static string ToHtmlDecode(this string text, int count = 1)
        {
            string result = null;
            for (int i = 0; i < count; i++)
            {
                result = HttpUtility.HtmlDecode(text);
            }
            return result;
        }

        public static string ToHtmlEncode(this string text, int count = 1)
        {
            string result = null;
            for (int i = 0; i < count; i++)
            {
                result = HttpUtility.HtmlEncode(text);
            }
            return result;
        }
    }
}
