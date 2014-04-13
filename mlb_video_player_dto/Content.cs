using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mlb_video_player_dto
{
    public class Content
    {
        #region 定数

        /// <summary>
        /// コンテンツのベースURL
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string CONTENT_BASE_URL = "https://www.youtube.com/watch?v={0}";

        /// <summary>
        /// サムネイルのベースURL
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string THUMBNAIL_BASE_URL = "https://i1.ytimg.com/vi/{0}/mqdefault.jpg";

        #endregion

        #region プロパティ

        /// <summary>
        /// コンテンツのURL
        /// </summary>
        public string ContentUrl { get; private set; }

        /// <summary>
        /// サムネイルのURL
        /// </summary>
        public string ThumbnailUrl { get; private set; }

        public string Title { get; private set; }

        public DateTime PublishedAt { get; private set; }

        public Team HomeTeam { get; private set; }

        public Team AwayTeam { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="li"></param>
        public Content(XElement li)
        {
            Initialize(li);
        }

        #endregion

        #region メソッド

        private void Initialize(XElement li)
        {
            var ns = li.Document.Root.Name.Namespace;
            var div = li.Descendants(ns + "div");

            var t = div.FirstOrDefault(e => e.ContainsAttribute("class", "thumbnail"));
            if (t != null)
            {
                var a = t.Descendants(ns + "a").FirstOrDefault();
                if (a != null && a.HasAttribute("href"))
                {
                    var id = a.Attribute("href").Value.Split('=').LastOrDefault();
                    this.ContentUrl = string.Format(CONTENT_BASE_URL, id);
                    this.ThumbnailUrl = string.Format(THUMBNAIL_BASE_URL, id);
                }
            }

            var c = div.FirstOrDefault(e => e.ContainsAttribute("class", "content"));
            if (c != null)
            {
                var a = c.Descendants(ns + "a").FirstOrDefault();
                if (a != null && a.HasAttribute("title"))
                {
                    this.Title = a.Attribute("title").Value;
                }
                if (!string.IsNullOrEmpty(this.Title))
                {
                    var dt = this.Title.Split(' ').Take(3).ToArray();
                    int year, month, day;
                    if (int.TryParse(dt[0], out month) && int.TryParse(dt[1], out day) && int.TryParse("20" + dt[2], out year))
                    {
                        this.PublishedAt = new DateTime(year, month, day);
                    }

                    var teams = this.Title.Split('@').ToArray();
                    var away = teams.FirstOrDefault().Split(' ').LastOrDefault().Trim();
                    var home = teams.LastOrDefault().Split(' ').FirstOrDefault().Trim();
                }
            }
        }

        #endregion
    }
}
