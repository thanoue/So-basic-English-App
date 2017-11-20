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
    public class dbLessonQuestion
    {
        DAFramework db;
        public dbLessonQuestion(string serverName)
        {
            db = new DAFramework(serverName);
        }
        public bool InsertLessnQuestion(ref string error,string userName, int dateProcess, string contentOfQuestion,DateTime timeOfAsk)
        {
            return db.ExcuteNoneQuery("spInsertLessonQuestion", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@userName", userName),
                new SqlParameter("@dateProcess", dateProcess),
                new SqlParameter("@contentOfQuestion",contentOfQuestion),
                new SqlParameter("@timeOfAsk",timeOfAsk)
                );
        }
        public bool UpdateAnswerOfLessonQuestion(ref string error,int Id, string contentOfAnswer)
        {
            return db.ExcuteNoneQuery("spUpdateAnswerOfLessonQuestion", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@Id", Id),
                new SqlParameter("@contenOfAnswer", contentOfAnswer)
                );
        }
        public DataTable GetLessonQuestionByDateProcess(int dateProcess)
        {
            return db.ExecuteQuery("spGetLessonQuestionByDateProcess", CommandType.StoredProcedure,
                new SqlParameter("@dateProcess", dateProcess));
        }
        public DataTable GetLessonQuestionByDateProcess_full(int dateProcess)
        {
            return db.ExecuteQuery("spGetLessonQuestionByDateProcess_full", CommandType.StoredProcedure,
                new SqlParameter("@dateProcess", dateProcess));
        }
    }
}
