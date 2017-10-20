using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFramework;
using System.Data;

namespace BusinessLogicFramework
{
  public  class dbDateProcess
    {
        DAFramework db;
        public dbDateProcess(string serverName)
        {
            db = new DAFramework(serverName);
        }
        public DataTable DatesprocessList()
        {
            return db.ExecuteQuery("spGetDatesProcess", CommandType.StoredProcedure);
        }
        public DataTable GetLevelList()
        {
            return db.ExecuteQuery("spGetProcessLevels", CommandType.StoredProcedure);
        }
        public string GetLessonByDateProcess(int dateProcess)
        {
            return db.GetLessonByDateProcess(dateProcess);
        }
    }
}
