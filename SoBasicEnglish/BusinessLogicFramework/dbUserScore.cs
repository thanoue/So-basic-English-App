using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFramework;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicFramework
{
    public class dbUserScore
    {
        DAFramework db;
        public dbUserScore(string serverName)
        {
            db = new DAFramework(serverName);
        }
        public int GetScoreToGainByLevel(int level)
        {
            return db.GetScoreToGainByLevel(level);
        }
        public int GetLevelByUserLoginName(string userLoginName)
        {
            return db.GetLevelByUserLoginName(userLoginName);
        }
        public int GetLevelScoreByUserLoginName(string userLoginName)
        {
            return db.GetLevelScoreByUserLoginName(userLoginName);
        }
        public DataTable GetChampionShip()
        {
            return db.ExecuteQuery("spGetChampionShip", CommandType.StoredProcedure);
        }
        public bool UpdateUserScore(ref string error, string loginName, int Score)
        {
            return db.ExcuteNoneQuery("spUpdateUserScore", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@loginName", loginName),
                new SqlParameter("@Score", Score)
                );
        }
    }
}
