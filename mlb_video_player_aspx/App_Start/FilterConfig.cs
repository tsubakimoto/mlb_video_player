using System.Web;
using System.Web.Mvc;

namespace mlb_video_player_aspx
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}