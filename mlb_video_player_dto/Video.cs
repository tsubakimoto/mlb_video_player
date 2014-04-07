using Google.Apis.YouTube.v3.Data;
using System;
using System.Diagnostics;

namespace mlb_video_player_dto
{
    public class Video
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string videoUrl = "https://www.youtube.com/watch?v={0}";

        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedAt { get; set; }

        public ThumbnailDetails Thumbnails { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string url = null;

        public string URL
        {
            get
            {
                if (string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(this.Id))
                {
                    url = string.Format(this.videoUrl, this.Id);
                }
                return url;
            }
            private set { url = value; }
        }

    }
}
