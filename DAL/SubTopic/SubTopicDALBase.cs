using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using InterviewQuestionPortal.Areas.MainTopic.Models;
using InterviewQuestionPortal.Areas.SubTopic.Models;

namespace InterviewQuestionPortal.DAL.SubTopic
{
    public class SubTopicDALBase:DALHelper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);

        #region Method : dbo_PR_SubTopic_SelectAll
        public DataTable dbo_PR_SubTopic_SelectAll()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SubTopic_SelectAll");

                DataTable dtSubTopic = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtSubTopic.Load(dr);
                }
                return dtSubTopic;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo.PR_Subtopic_DeleteByID
        public bool? dbo_PR_Subtopic_DeleteByID(int? SubtopicID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SubTopic_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "@SubTopicID", DbType.Int32, SubtopicID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == 1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_SubTopic_SelectByID
        public SubTopicModel dbo_PR_SubTopic_SelectByID(int SubTopicID)
        {
            SubTopicModel subTopicModel= new SubTopicModel();
            try
            {
                SqlDatabase sql = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sql.GetStoredProcCommand("dbo.PR_SubTopic_selectByID");
                sql.AddInParameter(dbCommand, "@SubTopicID", DbType.Int32, SubTopicID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sql.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    subTopicModel.SubTopicID = Convert.ToInt32(dataRow["SubTopicID"]);
                    subTopicModel.MainTopicID = Convert.ToInt32(dataRow["MainTopicID"]);
                    subTopicModel.SubTopicName = dataRow["SubTopicName"].ToString();
                    subTopicModel.SubjectID = Convert.ToInt32(dataRow["SubjectID"]);
                    //subTopicModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    subTopicModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                    subTopicModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                }
                return subTopicModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  Method : dbo.PR_SubTopic_Insert & dbo.PR_SubTopic_UpdateByid  name:dbo_PR_SubTopicTopic_Save
        public bool dbo_PR_SubTopicTopic_Save(SubTopicModel subTopicModel)
        {
            try
            {
                Console.WriteLine(subTopicModel.MainTopicID);
                if (subTopicModel.SubTopicID== 0 || subTopicModel.SubTopicID == null)
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.Pr_SubTopic_Insert");

                    sqlDB.AddInParameter(dbCMD, "@SubTopicName", DbType.String, subTopicModel.SubTopicName);
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, subTopicModel.SubjectID);
                    sqlDB.AddInParameter(dbCMD, "@MainTopicID", DbType.Int32, subTopicModel.MainTopicID);
                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, subTopicModel.UserID);
                    sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

                    bool isSuccess = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SubTopic_UpdateByID");
                    sqlDB.AddInParameter(dbCMD, "@SubTopicID", DbType.Int32, subTopicModel.SubTopicID);
                    sqlDB.AddInParameter(dbCMD, "@SubTopicName",DbType.String, subTopicModel.SubTopicName);
                    sqlDB.AddInParameter(dbCMD, "@MainTopicID", DbType.Int32, subTopicModel.MainTopicID);
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, subTopicModel.SubjectID);
                    //sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, subTopicModel.UserID);
                    //sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    //sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

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
