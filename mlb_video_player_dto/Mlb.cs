using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace mlb_video_player_dto
{
    public class Mlb
    {
        #region 定数

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string ESPN_TEAM_API_URL = "http://api.espn.com/v1/sports/baseball/mlb/teams?apikey={0}";

        #endregion

        #region プロパティ

        public List<Team> Teams { get; set; }

        #endregion

        #region コンストラクタ

        public Mlb()
        {
            Initialize();
        }

        #endregion

        #region メソッド

        private void Initialize()
        {
            //var apikey = GetApiKey();
            //var task = GetJson(apikey);
            //task.Wait();
            //var result = task.Result;

            var jobj = Test_GetJson();

            var sports = jobj.Properties().FirstOrDefault(p => p.Name == "sports");
            if (sports != null && sports.HasValues)
            {
                var sportsObj = (JObject)sports.Value.First;
                var leagues = sportsObj.Properties().FirstOrDefault(p => p.Name == "leagues");
                if (leagues != null && leagues.HasValues)
                {
                    var leaguesObj = (JObject)leagues.Value.First;
                    var teams = leaguesObj.Properties().FirstOrDefault(p => p.Name == "teams");

                    this.Teams = CreateTeams(teams);
                }
            }
        }

        private JObject Test_GetJson()
        {
            var json = string.Empty;
            using (var sr = new StreamReader("espn_api_teams.json", Encoding.Default))
            {
                json = sr.ReadToEnd();
            }
            return JObject.Parse(json);
        }

        private string GetApiKey()
        {
            using (var sr = new StreamReader("espn_api_key.txt", Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

        private async Task<JContainer> GetJson(string apikey)
        {
            var client = new WebClient();
            var url = string.Format(ESPN_TEAM_API_URL, apikey);
            var json = await client.DownloadStringTaskAsync(url);
            return await JsonConvert.DeserializeObjectAsync<JContainer>(json);
        }

        private List<Team> CreateTeams(JProperty teamsProps)
        {
            var teams = new List<Team>();

            foreach (JObject p in teamsProps.Value)
            {
                Console.WriteLine();
            }

            return teams;
        }

        #endregion
    }

    public class Team
    {
        #region プロパティ

        public string Name { get; set; }

        public TeamDivision Division { get; set; }

        #endregion

        #region 列挙型

        public enum TeamDivision
        {
            East,
            Central,
            West
        }

        #endregion

        #region コンストラクタ

        public Team()
        {

        }

        #endregion
    }
}
