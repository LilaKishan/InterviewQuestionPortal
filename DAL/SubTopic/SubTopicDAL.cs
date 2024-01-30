using InterviewQuestionPortal.Areas.Subject_Master.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using InterviewQuestionPortal.Areas.MainTopic.Models;
using InterviewQuestionPortal.Areas.SubTopic.Models;

namespace InterviewQuestionPortal.DAL.SubTopic
{
    public class SubTopicDAL : SubTopicDALBase
    {
        #region MainDropDownModel
        public List<MainTopicDropDownModel> MainTopicDropDown(int SubjectID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("MainTopic_Dropdown");
                DbCommand dbcmd = sqlDatabase.GetStoredProcCommand("dbo.MainTopic_Dropdown2");
                sqlDatabase.AddInParameter(dbCommand, "SubjectID", DbType.Int64, SubjectID);
                DataTable dataTable = new DataTable();


                using (IDataReader dataReader = sqlDatabase.ExecuteReader(SubjectID != 0 ? dbCommand : dbcmd))
                {
                    dataTable.Load(dataReader);
                }
                List<MainTopicDropDownModel> listOfMainTopic = new List<MainTopicDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    MainTopicDropDownModel mainTopicDropDownModel = new MainTopicDropDownModel();
                    mainTopicDropDownModel.MainTopicID = Convert.ToInt32(dataRow["MainTopicID"]);
                    mainTopicDropDownModel.MainTopicName = dataRow["MainTopicName"].ToString();
                    listOfMainTopic.Add(mainTopicDropDownModel);
                }
                return listOfMainTopic;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region SubTopicDropDownModel
        public List<SubTopicDropDownModel> SubTopicDropDown()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("SubTopic_Dropdown");
                //sqlDatabase.AddInParameter(dbCommand, "SubjectID", DbType.Int64, SubjectID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<SubTopicDropDownModel> listOfMainTopic = new List<SubTopicDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    SubTopicDropDownModel subTopicDropDownModel = new SubTopicDropDownModel();
                    subTopicDropDownModel.SubTopicID = Convert.ToInt32(dataRow["SubTopicID"]);
                    subTopicDropDownModel.SubTopicName = dataRow["SubTopicName"].ToString();
                    listOfMainTopic.Add(subTopicDropDownModel);
                }
                return listOfMainTopic;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


    }
}
