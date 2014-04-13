using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mlb_video_player_dto
{
    public class Mlb
    {
        #region プロパティ

        public List<League> Leagues { get; set; }

        #endregion

        #region コンストラクタ

        public Mlb()
        {

        }

        #endregion
    }

    public class League
    {
        #region プロパティ

        public string Name { get; set; }

        public LeagueType Type { get; set; }

        public List<Team> Teams { get; set; }

        #endregion

        #region 列挙型

        public enum LeagueType
        {
            American,
            National
        }

        #endregion

        #region コンストラクタ

        public League()
        {

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
