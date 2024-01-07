using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using InterviewQuestionPortal.Areas.MST_User.Models;

namespace InterviewQuestionPortal.DAL.MST_User
{
    public class MST_UserDALBase:DALHelper
    {
        #region PhotoFile Upload

        private readonly IWebHostEnvironment _webHostEnvironment;
        public MST_UserDALBase(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        #region dbo_PR_MST_User_SelectByUserNamePassword
        public DataTable dbo_PR_MST_User_SelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("PR_User_Master_selectByUsernamePassword");
                sqlDB.AddInParameter(dbCommand, "@UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCommand, "@Password", SqlDbType.VarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCommand))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex) { return null; }
        }
        #endregion

        #region  dbo_PR_MST_User_Register
        public bool dbo_PR_MST_User_Register(MST_UserModel mST_UserModel)
        {
            try
            {

                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_User_Master_SelectByUsername");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, mST_UserModel.UserName);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCMD))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable.Rows.Count > 0)
                {
                    return false;
                }
                else
                {// For Upload and get Photo into Database
                    if (mST_UserModel.CoverPhoto != null)
                    {
                        string folder = "user/photos/";
                        folder += Guid.NewGuid().ToString() + "_" + mST_UserModel.CoverPhoto.FileName;  // Guid.NewGuid().ToString() for making file name unique

                        mST_UserModel.ImageURL = "/" + folder;

                        string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                        mST_UserModel.CoverPhoto.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                    }
                    DbCommand dbCMD1 = sqlDB.GetStoredProcCommand("PR_User_Master_Insert");
                    sqlDB.AddInParameter(dbCMD1, "@UserName", SqlDbType.VarChar, mST_UserModel.UserName);
                    sqlDB.AddInParameter(dbCMD1, "@Password", SqlDbType.VarChar, mST_UserModel.Password);
                    sqlDB.AddInParameter(dbCMD1, "@FirstName", SqlDbType.VarChar, mST_UserModel.FirstName);
                    sqlDB.AddInParameter(dbCMD1, "@LastName", SqlDbType.VarChar, mST_UserModel.LastName);
                    sqlDB.AddInParameter(dbCMD1, "@ImageURL", SqlDbType.VarChar, mST_UserModel.ImageURL);
                    sqlDB.AddInParameter(dbCMD1, "@Email", SqlDbType.VarChar, mST_UserModel.Email);
                    sqlDB.AddInParameter(dbCMD1, "Created", SqlDbType.DateTime, DBNull.Value);
                    sqlDB.AddInParameter(dbCMD1, "Modified", SqlDbType.DateTime, DBNull.Value);
                    Console.WriteLine(dbCMD1);
                    if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD1)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex) { return false; }
        }
        #endregion

        #region Method: PR_User_SelectAll

        public DataTable PR_MST_USER_SELECTALL()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Master_selectall");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Method: PR_User_Delete

        public bool PR_MST_USER_DELETEBYID(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("dbo.PR_User_Master_DeleteByID");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        #endregion
    }
}
