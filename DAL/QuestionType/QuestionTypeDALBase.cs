using InterviewQuestionPortal.Areas.QuestionType.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace InterviewQuestionPortal.DAL.QuestionType
{
    public class QuestionTypeDALBase:DALHelper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);

        #region Method: dbo_PR_QuestionType_SelectAll
        public DataTable dbo_PR_QuestionType_SelectAll()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_QuestionType_selectall");

                DataTable dtQuestionType = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtQuestionType.Load(dr);
                }
                return dtQuestionType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  Method : dbo.PR_QuestionType_Insert & dbo.PR_QuestionType_UpdateByid  name:dbo_PR_QuestionType_Save
        public bool dbo_QuestionType_Save(QuestionTypeModel questionTypeModel)
        {
            try
            {
                Console.WriteLine(questionTypeModel.QuestionTypeID);
                if (questionTypeModel.QuestionTypeID == 0)
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.Pr_QuestionType_Insert");
                    sqlDB.AddInParameter(dbCMD, "QuestionType", DbType.String, questionTypeModel.QuestionType);
                    sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

                    sqlDB.ExecuteNonQuery(dbCMD);
                    return true;
                }
                else
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_QuestionType_UpdateByid");
                    sqlDB.AddInParameter(dbCMD, "@QuestionTypeID", DbType.Int32, questionTypeModel.QuestionTypeID);
                    sqlDB.AddInParameter(dbCMD, "@QuestionType", DbType.String, questionTypeModel.QuestionType);
                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, questionTypeModel.UserID);
                    //sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    //sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

                    sqlDB.ExecuteNonQuery(dbCMD);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region Method : dbo.PR_QuestionType_SelectByID
        public QuestionTypeModel dbo_PR_QuestionType_SelectByID(int? questionTypeID)
        {
            QuestionTypeModel questionTypeModel = new QuestionTypeModel();
            try
            {
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("dbo.PR_QuestionType_SelectByID");
                sqlDB.AddInParameter(dbCommand, "@QuestionTypeID", DbType.Int32, questionTypeID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                   questionTypeModel.QuestionTypeID = Convert.ToInt32(dataRow["QuestionTypeID"]);
                    questionTypeModel.QuestionType = dataRow["QuestionType"].ToString();
                    // QuestionTypeModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    questionTypeModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                    questionTypeModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                }
                return questionTypeModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo.PR_QuestionType_DeleteByID
        public bool? dbo_PR_QuestionType_DeleteByID(int questionTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_QuestionType_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "@QuestionTypeID", DbType.Int32, questionTypeID);

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
