using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFramework
{
   public  class DAFramework
    {
        private string sqlConnect;
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        public DAFramework(string sername)
        {
            sqlConnect = @"data source =" + sername + "; initial catalog =SoBasicEnglishApp; integrated security = true";
          //  sqlConnect = @"Server=tcp:trankhaserver.database.windows.net,1433;Initial Catalog=demo;Persist Security Info=False;User ID=thanoue;Password=Namidth123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            conn = new SqlConnection(sqlConnect);
            cmd = conn.CreateCommand();
        }
        public int GetMaxLessonIndexByLevelId(int levelId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Max(turnNumber) from NgayHoc where NgayHoc.processLevel='" + levelId + "'", conn);
            int kq = 0;
            try
            {

                kq = (int)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return 0;
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public int GetScoreToGainByLevel(int userLevel)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select scoreToGain from UserLevel where userLevel= '" + userLevel + "'", conn);
            int kq = 0;
            try
            {

                kq = (int)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return 0;
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public int GetLevelByUserLoginName(string userLoginName)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select userLevel from NguoiDung where loginName= '" + userLoginName + "'", conn);
            int kq = 0;
            try
            {

                kq = (int)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return 0;
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public int GetLevelScoreByUserLoginName(string userLoginName)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select levelScore from NguoiDung where loginName= '" + userLoginName + "'", conn);
            int kq = 0;
            try
            {

                kq = (int)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return 0;
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public string GetBasicInfoByUserLoginName(string userLoginName)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select basicInfo from NguoiDung where loginName= '" + userLoginName + "'", conn);
            string kq = "";
            try
            {

                kq = (string)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return "";
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public string GetLessonNoteByUserLoginName_Date(string userLoginName,int dateprocess)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select contentOfNote from GhiChu_BaiHoc_NguoiDung where userLoginName= '" + userLoginName + "' and  dateProcess = '"+dateprocess+"'  ", conn);
            string kq = "";
            try
            {

                kq = (string)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return "";
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public string GetUserNameByUserLoginName(string userLoginName)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select userName from NguoiDung where loginName= '" + userLoginName + "'", conn);
            string kq = "";
            try
            {

                kq = (string)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return "";
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public int GetprocessByUserLoginName(string userLoginName)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select process from NguoiDung where loginName= '" + userLoginName + "'", conn);
            int kq = 0;
            try
            {

                kq = (int)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return 0;
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public int GetRoleByUserLoginName(string userLoginName)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select userRole from NguoiDung where loginName= '" + userLoginName + "'", conn);
            int kq = 0;
            try
            {

                kq = (int)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return 0;
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public string GetLessonByDateProcess(int dateProcess)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select detailInfo from NgayHoc where turnNumber= " + dateProcess , conn);
            string kq = "";
            try
            {

                kq = (string)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return "";
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public string GetPassWordByUserLoginName(string userLoginName, string email)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select loginPassWord from NguoiDung where loginName= '" + userLoginName + "' and Email = '"+ email +"'" , conn);
            string kq = "";
            try
            {

                kq = (string)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return null;
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public byte[] GetGrammarWordFileByID(int turnNumber)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select gramContent from LyThuyetNguPhap where Id= '" + turnNumber + "'", conn);
            byte[] kq;
            kq = (byte[])cmd.ExecuteScalar();
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public byte[] GetUserAVTByUserLoginName(string userLoginName)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select userAvatar from NguoiDung where loginName= '" + userLoginName + "'", conn);
            byte[] kq;
            kq = (byte[])cmd.ExecuteScalar();
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return  kq;
                    
        }
        public byte[] GetAudioFileById(int turnNumber)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("select audioKey from AudioBaiTapNghe where Id= '" + turnNumber + "'", conn);
            byte[] kq;
            kq = (byte[])cmd.ExecuteScalar();
             if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public string Login(string userName,string passWord)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();

            SqlCommand cmd = new SqlCommand("select loginName from NguoiDung where loginName= '" + userName+"'and loginPassWord='"+ passWord+"'" , conn);
            string kq = "";
            try
            {

              kq = (string)cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                return "";
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return kq;
        }
        public bool ExcuteNoneQuery(string cmdText, CommandType cmdType, ref string error, params SqlParameter[] sqlParam)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 60;
            cmd.Parameters.Clear();

            foreach (SqlParameter param in sqlParam)
                cmd.Parameters.Add(param);

            bool excuteSuccess = false;
            try
            {
                cmd.ExecuteNonQuery();
                excuteSuccess = true;
            }
            catch (SqlException sqlException)
            {
                error = sqlException.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return excuteSuccess;

        }
        public DataTable ExecuteQuery(string cmdText, CommandType cmdType, params SqlParameter[] sqlParam)
        {

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 60;
            cmd.Parameters.Clear();

            try
            {
                foreach (SqlParameter param in sqlParam)
                    cmd.Parameters.Add(param);

            }
            catch (Exception)
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            da = new SqlDataAdapter(cmd);
            if (conn.State == ConnectionState.Open)
                conn.Close();
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
