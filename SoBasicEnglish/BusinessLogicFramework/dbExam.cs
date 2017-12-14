using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFramework;

namespace BusinessLogicFramework
{
    public class dbExam
    {
        DAFramework db;
        public dbExam(string serverName)
        {
            db = new DAFramework(serverName);
        }
        public int GetProcessLevel(int process)
        {
            return db.GetProcessLevel(process);
        }

    }
}
