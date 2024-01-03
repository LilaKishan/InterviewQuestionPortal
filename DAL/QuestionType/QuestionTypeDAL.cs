using InterviewQuestionPortal.Areas.Subject_Master.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using InterviewQuestionPortal.Areas.QuestionType.Models;

namespace InterviewQuestionPortal.DAL.QuestionType
{
    public class QuestionTypeDAL:QuestionTypeDALBase
    {
        #region QuestionTypeDropdown
        public List<QuestionTypeDropdownModel> QuestionTypeDropdown()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("QuestionType_Dropdown");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<QuestionTypeDropdownModel> listOfQuestionType = new List<QuestionTypeDropdownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    QuestionTypeDropdownModel questionTypeDropdownModel = new QuestionTypeDropdownModel();
                    questionTypeDropdownModel.QuestionTypeID = Convert.ToInt32(dataRow["QuestionTypeID"]);
                    questionTypeDropdownModel.QuestionType = dataRow["QuestionType"].ToString();
                    listOfQuestionType.Add(questionTypeDropdownModel);
                }
                return listOfQuestionType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
