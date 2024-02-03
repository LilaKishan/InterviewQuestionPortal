using InterviewQuestionPortal.Areas.Question_Master.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace InterviewQuestionPortal.DAL.Question_Master
{
    public class Question_MasterDALBase:DALHelper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);

        #region Method : dbo_PR_Question_Master_SelectAll
        public DataTable dbo_PR_Question_Master_SelectAll()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Question_Master_SelectAll");

                DataTable dtQuestion_Master = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtQuestion_Master.Load(dr);
                }
                return dtQuestion_Master;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo.PR_Question_Master_DeleteByID
        public bool? dbo_PR_Question_Master_DeleteByID(int? QuestionID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Question_Master_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "@QuestionID", DbType.Int32, QuestionID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == 1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_Question_Master_SelectByID
        public Question_MasterModel dbo_PR_Question_Master_SelectByID(int? QuestionID)
        {
            Question_MasterModel question_MasterModel = new Question_MasterModel();
            try
            {
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("dbo.PR_Question_Master_SelectByID");
                sqlDB.AddInParameter(dbCommand, "@QuestionID", DbType.Int32, QuestionID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    question_MasterModel.QuestionID = Convert.ToInt32(dataRow["QuestionID"]);
                    question_MasterModel.Question = dataRow["Question"].ToString();
                    question_MasterModel.Option_A = dataRow["Option_A"].ToString();
                    question_MasterModel.Option_B = dataRow["Option_B"].ToString();
                    question_MasterModel.Option_C = dataRow["Option_C"].ToString();
                    question_MasterModel.Option_D = dataRow["Option_D"].ToString();
                    question_MasterModel.TrueOption = dataRow["TrueOption"].ToString();
                    question_MasterModel.CorrectAnswer = dataRow["CorrectAnswer"].ToString();
                    question_MasterModel.Description = dataRow["Description"].ToString();
                    question_MasterModel.SubjectID = Convert.ToInt32(dataRow["SubjectID"]);
                    question_MasterModel.MainTopicID = Convert.ToInt32(dataRow["MainTopicID"]);
                    question_MasterModel.SubTopicID = Convert.ToInt32(dataRow["SubTopicID"]);
                    question_MasterModel.QuestionTypeID = Convert.ToInt32(dataRow["QuestionTypeID"]);
                   // question_MasterModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    question_MasterModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                    question_MasterModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                }
                return question_MasterModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  Method : dbo.PR_Question_Master_Insert & dbo.PR_Question_Master_UpdateByid  name:dbo_PR_Question_MasterTopic_Save
        public bool dbo_PR_Question_MasterTopic_Save(Question_MasterModel question_MasterModel)
        {
            try
            {
                Console.WriteLine(question_MasterModel.QuestionID);
                if (question_MasterModel.QuestionID == 0 || question_MasterModel.QuestionID == null)
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.Pr_Question_Master_Insert");

                    sqlDB.AddInParameter(dbCMD, "@Question", DbType.String, question_MasterModel.Question);
                    sqlDB.AddInParameter(dbCMD, "@Option_A", DbType.String, question_MasterModel.Option_A);
                    sqlDB.AddInParameter(dbCMD, "@Option_B", DbType.String, question_MasterModel.Option_B);
                    sqlDB.AddInParameter(dbCMD, "@Option_C", DbType.String, question_MasterModel.Option_C);
                    sqlDB.AddInParameter(dbCMD, "@Option_D", DbType.String, question_MasterModel.Option_D);
                    sqlDB.AddInParameter(dbCMD, "@TrueOption", DbType.String, question_MasterModel.TrueOption);
                    sqlDB.AddInParameter(dbCMD, "@CorrectAnswer", DbType.String, question_MasterModel.CorrectAnswer);
                    sqlDB.AddInParameter(dbCMD, "@Description", DbType.String, question_MasterModel.Description);
                    sqlDB.AddInParameter(dbCMD, "@QuestionTypeID", DbType.Int32, question_MasterModel.QuestionTypeID);
                    sqlDB.AddInParameter(dbCMD, "@SubTopicID", DbType.Int32, question_MasterModel.SubTopicID);
                    sqlDB.AddInParameter(dbCMD, "@MainTopicID", DbType.Int32,question_MasterModel.MainTopicID);
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, question_MasterModel.SubjectID);
                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, question_MasterModel.UserID);
                    sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

                    bool isSuccess = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Question_Master_UpdateByID");
                    sqlDB.AddInParameter(dbCMD, "@QuestionID", DbType.Int32, question_MasterModel.QuestionID);
                    sqlDB.AddInParameter(dbCMD, "@Question", DbType.String, question_MasterModel.Question);
                    sqlDB.AddInParameter(dbCMD, "@Option_A", DbType.String, question_MasterModel.Option_A);
                    sqlDB.AddInParameter(dbCMD, "@Option_B", DbType.String, question_MasterModel.Option_B);
                    sqlDB.AddInParameter(dbCMD, "@Option_C", DbType.String, question_MasterModel.Option_C);
                    sqlDB.AddInParameter(dbCMD, "@Option_D", DbType.String, question_MasterModel.Option_D);
                    sqlDB.AddInParameter(dbCMD, "@TrueOption", DbType.String, question_MasterModel.TrueOption);
                    sqlDB.AddInParameter(dbCMD, "@CorrectAnswer", DbType.String, question_MasterModel.CorrectAnswer);
                    sqlDB.AddInParameter(dbCMD, "@Description", DbType.String, question_MasterModel.Description);
                    sqlDB.AddInParameter(dbCMD, "@QuestionTypeID", DbType.Int32, question_MasterModel.QuestionTypeID);
                    sqlDB.AddInParameter(dbCMD, "@SubTopicID", DbType.Int32, question_MasterModel.SubTopicID);
                    sqlDB.AddInParameter(dbCMD, "@MainTopicID", DbType.Int32, question_MasterModel.MainTopicID);
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, question_MasterModel.SubjectID);
                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, question_MasterModel.UserID);
                   // sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
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
