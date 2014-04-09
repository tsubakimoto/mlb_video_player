using System;

namespace mlb_video_player_console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //YouTubeSearch();
            YouTubePlayLists();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void YouTubeSearch()
        {
            Console.WriteLine("YouTube Data API: Search");
            Console.WriteLine("========================");

            try
            {
                Search.Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        static void YouTubePlayLists()
        {
            Console.WriteLine("YouTube Data API: Playlist Updates");
            Console.WriteLine("==================================");

            try
            {
                new PlayLists().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
}
