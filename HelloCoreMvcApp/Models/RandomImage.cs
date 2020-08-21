﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HelloCoreMvcApp.Models
{
    public class RandomImage
    {
        public string ImageUrl { get; }

        public RandomImage(string keyword)
        {
            ImageUrl = GetRandomImageUrl(keyword);
        }

        private string GetRandomImageUrl(string keyword)
        {
            var rnd = new Random();
            string html = GetHtmlCode(keyword);
            List<string> urls = GetUrls(html);

            return urls[rnd.Next(0, urls.Count - 1)];
        }

        private string GetHtmlCode(string keyword)
        {
            var rnd = new Random();

            string url = "https://www.google.com/search?q=" + keyword + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }

        private static List<string> GetUrls(string html)
        {
            var urls = new List<string>();

            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
                ndx++;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
            }
            return urls;
        }
    }
}
