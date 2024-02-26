using InterviewQuestionPortal.Areas.Question_Master.Models;
using System.Data.Common;
using System.Data;
using InterviewQuestionPortal.Areas.MST_Test.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using InterviewQuestionPortal.BAL;

namespace InterviewQuestionPortal.DAL.MST_Test
{
    public class MST_TestDALBase:DALHelper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);

        #region  Method : dbo.PR_Generate_Test_Insert name:dbo_PR_Generate_Test_Save
        public bool dbo_PR_Generate_Test_Save(MST_TestModel mST_TestModel)
        {
            try
            {
                Console.WriteLine(mST_TestModel.TestID);
                //if (mST_TestModel.TestID== 0 || mST_TestModel.TestID== null)
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Generate_Test_Insert");

                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32,CV.UserID());
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, mST_TestModel.SubjectID);
                    sqlDB.AddInParameter(dbCMD, "@MainTopicID", DbType.Int32, mST_TestModel.MainTopicID);
                    sqlDB.AddInParameter(dbCMD, "@SubTopicID", DbType.Int32, mST_TestModel.SubTopicID);
                    sqlDB.AddInParameter(dbCMD, "@Duration", DbType.Int32, mST_TestModel.Duration);
                    sqlDB.AddInParameter(dbCMD,"@Total_Questions",DbType.Int32,mST_TestModel.Total_Questions);
                    sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);
                 
                    bool isSuccess = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                    if (!isSuccess)
                    {
                        DataTable dtQuestion = new DataTable();

                        using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                        {
                            dtQuestion.Load(dr);
                        }
                        if (dtQuestion.Rows.Count > 0)
                        {
                            isSuccess = false;
                        }
                    }
                    return isSuccess;
                }
               // return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : dbo_PR_TestQuestion_selectByUserID
        public DataTable dbo_PR_TestQuestion_selectByUserID()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_TestQuestion_selectByUserID");
                sqlDB.AddInParameter(dbCMD,"UserID",DbType.Int32,CV.UserID());
                DataTable dtQuestion = new DataTable();
              //  TestQuestionModel model = new TestQuestionModel();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtQuestion.Load(dr);
                }
                return dtQuestion;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
