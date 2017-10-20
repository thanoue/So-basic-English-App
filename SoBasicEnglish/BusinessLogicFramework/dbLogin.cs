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
    public class dbLogin
    {
        DAFramework db;
        public dbLogin(string serverName)
        {
            db = new DAFramework(serverName);
        }
        public string Login(string userName, string passWord)
        {
            return db.Login(userName, passWord);
        }
        public int GetRoleByUserLoginName(string userLoginName)
        {
            return db.GetRoleByUserLoginName(userLoginName);
        } 
        public string GetUserNameByUserLoginName(string userLoginName)
        {
            return db.GetUserNameByUserLoginName(userLoginName);
        }
        public string GetPassWordByUserLoginName(string userLoginName,string emailAddress)
        {
            return db.GetPassWordByUserLoginName(userLoginName, emailAddress);
        }
        public byte[] GetUserAVT(string userLoginName)
        {
            return db.GetUserAVTByUserLoginName(userLoginName);
        }
        public DataTable GetUserList()
        {
            return db.ExecuteQuery("spGetUserList", CommandType.StoredProcedure);
        }
        public bool SignUp(ref string error, string loginName, string loginPassword, string Email, string userName, string basicInfo, byte[] userAvatar)
        {
            return db.ExcuteNoneQuery("spInsertNguoiDung", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@loginName", loginName),
                new SqlParameter("@loginPassword", loginPassword),
                 new SqlParameter("@Email", Email),
                  new SqlParameter("@userName", userName),
                   new SqlParameter("@basicInfo", basicInfo),
                    new SqlParameter("@userAvatar", userAvatar)
                );
        }
        public bool ChangTheRole(ref string error, string loginName, int roleId)
        {
            return db.ExcuteNoneQuery("spChangetheRole", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@userLoginName", loginName),
                    new SqlParameter("@roleId", roleId)
                );
        }

    }
}
