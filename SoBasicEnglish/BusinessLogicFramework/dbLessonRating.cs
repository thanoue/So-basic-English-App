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
   public class dbLessonRating
    {
        DAFramework db;
        public dbLessonRating(string serverName)
        {
            db = new DAFramework(serverName);

        }
        public bool InsertLessonRating(ref string error, int dayProcess,string userLoginName,int rating, string feedBackContent)
        {
            return db.ExcuteNoneQuery("spInsertLessonRating", CommandType.StoredProcedure,
               ref error,
                new SqlParameter("@dateProcess", dayProcess),
                new SqlParameter("@userLoginName", userLoginName),
                   new SqlParameter("@rating", rating),
                new SqlParameter("@feedBack", feedBackContent)

               );
        }
        public bool UpdateLessonRating(ref string error, int dayProcess, string userLoginName, int rating, string feedBackContent)
        {
            return db.ExcuteNoneQuery("spUpdateLessonRating", CommandType.StoredProcedure,
               ref error,
                new SqlParameter("@turnNumber", dayProcess),
                new SqlParameter("@userLoginName", userLoginName),
                   new SqlParameter("@rating", rating),
                new SqlParameter("@feedBack", feedBackContent)

               );
        }
        public DataTable GeLessonRatingById(int dateProcess)
        {
            return db.ExecuteQuery("spGeLessonRatingById", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", dateProcess));
        }
    }
}
