using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sgml;
using System.Xml.Linq;
using System.IO;
using mlb_video_player_dto;

namespace mlb_video_player_console
{
    public static class Scraping
    {
        private static XDocument ParseHtml(string href)
        {
            using (var sgml = new SgmlReader { Href = href })
            {
                return XDocument.Load(sgml);
            }
        }

        public static void Run()
        {
            var xml = ParseHtml("https://www.youtube.com/user/agsgdgfg01/videos");

            var ns = xml.Root.Name.Namespace;

            var liList = xml.Descendants(ns + "ul")
                        .Where(e => e.HasAttribute("id", "channels-browse-content-grid"))
                        .SelectMany(e => e.Descendants(ns + "li"))
                        .Where(e => e.ContainsAttribute("class", "channels-content-item"));

            foreach (var li in liList)
            {
                var div = li.Descendants(ns + "div");

                //var t = div.FirstOrDefault(e => e.ContainsAttribute("class", "thumbnail"));
                //if (t != null)
                //{
                //    var thumbnail = new Thumbnail(t);
                //}

                //var c = div.FirstOrDefault(e => e.ContainsAttribute("class", "content"));

                Console.WriteLine();
            }
        }
    }
}
