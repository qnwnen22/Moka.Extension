using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Moka.Extension
{
    public static class CookieExtension
    {
        public static CookieCollection GetCollection(this CookieContainer cookieContainer)
        {
            CookieCollection cookieCollection = new CookieCollection();
            Hashtable table = (Hashtable)cookieContainer.GetType().InvokeMember("m_domainTable",
                BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, cookieContainer, new object[] { });

            foreach (var item in table.Keys)
            {
                string key = (string)item;

                if (key[0] == '.')
                    key = key.Substring(1);

                SortedList list = (SortedList)table[item].GetType().InvokeMember("m_list",
                    BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, table[item], new object[] { });

                foreach (var listKey in list.Keys)
                {
                    string url = "https://" + key + (string)listKey;
                    cookieCollection.Add(cookieContainer.GetCookies(new Uri(url)));
                }
            }
            return cookieCollection;
        }

        public static List<Cookie> GetCookies(this CookieContainer cookieContainer)
        {
            var collection = GetCollection(cookieContainer);
            var cookies = new List<Cookie>();
            foreach (Cookie cookie in collection)
            {
                cookies.Add(cookie);
            }
            return cookies;
        }
    }
}
