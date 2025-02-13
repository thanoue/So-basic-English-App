﻿using System;
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
        public int GetProcessByUserLoginName(string userLoginName)
        {
            return db.GetprocessByUserLoginName(userLoginName);
        }
        public string GetUserNameByUserLoginName(string userLoginName)
        {
            return db.GetUserNameByUserLoginName(userLoginName);
        }
        public string GetBasicInfoByUserLoginName(string userLoginName)
        {
            return db.GetBasicInfoByUserLoginName(userLoginName);
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
        public bool UpdateUserProfile(ref string error, string loginName, string userFullName, string basicInfo, byte[] userAvatar)
        {
            return db.ExcuteNoneQuery("spUpdateUserProfile", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@userLoginName", loginName),
                new SqlParameter("@userAvt", userAvatar),
                 new SqlParameter("@userFullName", userFullName),
                   new SqlParameter("@userbasicInfo", basicInfo)
                );
        }
        public bool UpdateUserPassword(ref string error, string loginName, string userNewPassword)
        {
            return db.ExcuteNoneQuery("spUpdateUserPassword", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@userLoginName", loginName),
                new SqlParameter("@userNewPassword", userNewPassword)
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
        public bool UpdateUserProcess(ref string error,string loginName)
        {
            return db.ExcuteNoneQuery("spUpdateUserProcess", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@userLoginName", loginName)
                );
        }
        public bool DeleteUser(ref string error, string loginName)
        {
            return db.ExcuteNoneQuery("spDeleteUser", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@loginName", loginName)
                );
        }

    }
}
