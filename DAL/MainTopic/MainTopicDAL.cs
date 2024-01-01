using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using InterviewQuestionPortal.Areas.Subject_Master.Models;

namespace InterviewQuestionPortal.DAL.MainTopic
{
    public class MainTopicDAL:MainTopicDALBase
    {
        #region SubjectDropDownModel
        public List<SubjectDropDownModel> SubjectDropDown()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("subject_Dropdown");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<SubjectDropDownModel> listOfSubject = new List<SubjectDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    SubjectDropDownModel subjectDropDownModel = new SubjectDropDownModel();
                    subjectDropDownModel.SubjectID = Convert.ToInt32(dataRow["SubjectID"]);
                    subjectDropDownModel.SubjectName = dataRow["SubjectName"].ToString();
                    listOfSubject.Add(subjectDropDownModel);
                }
                return listOfSubject;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
