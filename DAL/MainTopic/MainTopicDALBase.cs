using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using InterviewQuestionPortal.Areas.MainTopic.Models;
using InterviewQuestionPortal.Areas.Subject_Master.Models;

namespace InterviewQuestionPortal.DAL.MainTopic
{
    public class MainTopicDALBase : DALHelper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);

        #region Method : dbo_PR_MainTopic_SelectAll
        public DataTable dbo_PR_MainTopic_SelectAll()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MainTopic_SelectAll");

                DataTable dtMainTopic = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtMainTopic.Load(dr);
                }
                return dtMainTopic;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo.PR_MainTopic_DeleteByID
        public bool? dbo_PR_MainTopic_DeleteByID(int? MainTopicID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MainTopic_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "@MainTopicID", DbType.Int32, MainTopicID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == 1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_MainTopic_SelectByID
        public MainTopicModel dbo_PR_MainTopic_SelectByID(int? MainTopicID)
        {
            MainTopicModel mainTopicModel = new MainTopicModel();
            try
            {
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("dbo.PR_MainTopic_SelectByID");
                sqlDB.AddInParameter(dbCommand, "@MainTopicID", DbType.Int32, MainTopicID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    mainTopicModel.MainTopicID = Convert.ToInt32(dataRow["MainTopicID"]);
                    mainTopicModel.MainTopicName = dataRow["MainTopicName"].ToString();
                    mainTopicModel.SubjectID = Convert.ToInt32(dataRow["SubjectID"]);
                    //mainTopicModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    mainTopicModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                   mainTopicModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                }
                return mainTopicModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  Method : dbo.PR_MainTopic_Insert & dbo.PR_MainTopic_UpdateByid  name:dbo_PR_MainTopic_Save
        public bool dbo_PR_MainTopic_Save(MainTopicModel mainTopicModel)
        {
            try
            {
                //Console.WriteLine(mainTopicModel.MainTopicID);
                if (mainTopicModel.MainTopicID == 0)
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.Pr_MainTopic_Insert");
                    sqlDB.AddInParameter(dbCMD, "MainTopicName", SqlDbType.VarChar, mainTopicModel.MainTopicName);
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, mainTopicModel.SubjectID);
                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, mainTopicModel.UserID);
                    sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

                    bool isSuccess = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MainTopic_UpdateByID");
                    sqlDB.AddInParameter(dbCMD, "@MainTopicID", DbType.Int32, mainTopicModel.MainTopicID);
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, mainTopicModel.SubjectID);
                    sqlDB.AddInParameter(dbCMD, "@MainTopicName", DbType.String, mainTopicModel.MainTopicName);
                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, mainTopicModel.UserID);
                    //sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    //  sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

                    bool isSuccess = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

    }
}
