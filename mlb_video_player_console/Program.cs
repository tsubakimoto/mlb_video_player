using Google.Apis.YouTube.v3;
using google_api_credential;
using mlb_video_player_dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlb_video_player_console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("YouTube Data API: Search");
            Console.WriteLine("========================");

            try
            {
                new Program().SearchRun().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private async Task SearchRun()
        {
            // 認証情報の設定
            var youtubeService = new YouTubeService(Credential.GetInitializer());

            // 検索条件の設定
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "mlb cg";
            searchListRequest.MaxResults = 50;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchListRequest.PublishedAfter = DateTime.Today;
            searchListRequest.Type = "video";
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True;

            // 検索処理の非同期実行
            var searchListResponse = await searchListRequest.ExecuteAsync();

            // 検索結果の展開
            var videos =
                searchListResponse
                    .Items
                    .Where(item => item.Id.Kind == "youtube#video")
                    .Select(item => new Video()
                                    {
                                        Id = item.Id.VideoId,
                                        Title = item.Snippet.Title,
                                        PublishedAt = item.Snippet.PublishedAt,
                                        Thumbnails = item.Snippet.Thumbnails
                                    });
#if false
            var videos = new List<string>();
            var channels = new List<string>();
            var playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }

            Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
#endif
        }
    }
}
