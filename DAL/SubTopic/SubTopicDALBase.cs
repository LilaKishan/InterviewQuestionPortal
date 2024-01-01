using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

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
    }
}
