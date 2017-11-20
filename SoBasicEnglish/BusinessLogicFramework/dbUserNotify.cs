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
   public  class dbUserNotify
    {
        DAFramework db;
        public dbUserNotify(string servername)
        {
            db = new DAFramework(servername);
        }
        public bool InsertNotify(ref string error, string userLoginName, string contentOfNotify)
        {
            return db.ExcuteNoneQuery("spInsertNotify", CommandType.StoredProcedure,
                ref error,
               new SqlParameter("@userLoginName", userLoginName),
               new SqlParameter("@contentOfNotify",contentOfNotify));
        }
        public DataTable GetNotAnsweredyetNotify(string userLoginName)
        {
            return db.ExecuteQuery("spGetNotAnsweredyetNotify", CommandType.StoredProcedure,
                new SqlParameter("@userLoginName", userLoginName));
        }
    }
}
