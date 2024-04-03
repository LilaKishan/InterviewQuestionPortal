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
        public int dbo_PR_Generate_Test_Save(MST_TestModel mST_TestModel)
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
                   

                    int isSuccess=-1; 
                    {
                        DataTable dtQuestion = new DataTable();

                        using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                        {
                            dtQuestion.Load(dr);
                        }
                        if (dtQuestion.Rows.Count > 0)
                        {
                            foreach(DataRow dataRow in dtQuestion.Rows)
                            {
                                if (Convert.ToInt32(dataRow["testID"].ToString()) != -1 && Convert.ToInt32(dataRow["testID"].ToString()) != 0)
                                {

                                    isSuccess = Convert.ToInt32(dataRow["testID"].ToString());

                                } 
                                break;
                            } 
                        }
                    }
                    return isSuccess;
                }
               
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region Method : dbo_PR_TestQuestion_selectByUserIDAndTestID
        //public DataTable dbo_PR_TestQuestion_selectByUserID(int testid)
        //{
        //    try
        //    {
                
        //        DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_TestQuestion_selectByUserID");
        //        sqlDB.AddInParameter(dbCMD,"UserID",DbType.Int32,CV.UserID());
        //        sqlDB.AddInParameter(dbCMD, "Testid", DbType.Int32, testid);
        //        DataTable dtQuestion = new DataTable();
             
        //        using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
        //        {
        //            dtQuestion.Load(dr);
        //        }
        //        return dtQuestion;

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        #endregion

        #region Method : PR_Test_Attempt_SelectQuestionByUserIDAndTestID
        public DataTable PR_Test_Attempt_SelectQuestionByUserIDAndTestID(int testid)
        {
            try
            {

                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Test_Attempt_SelectQuestionByUserIDAndTestID");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "Testid", DbType.Int32, testid);
                DataTable dtQuestion = new DataTable();

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

        #region PR_Test_Attempt_UpdateByTestIDAndUserID
        public bool PR_Test_Attempt_UpdateByTestIDAndUserID(int testid,int testQuestionID,string trueoption)
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Test_Attempt_UpdateByTestIDAndUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "Testid", DbType.Int32, testid);
                sqlDB.AddInParameter(dbCMD, "testQuestionID", DbType.Int32, testQuestionID);
                sqlDB.AddInParameter(dbCMD, "trueoption", DbType.String, trueoption); 
                
                bool isSuccess = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                return isSuccess;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 

        #region Testlist
        public DataTable dbo_PR_Generate_Test_Selectall()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Generate_Test_selectByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, CV.UserID());
                DataTable dtQuestion = new DataTable();
               
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

        #region Method: pr_test_attempt_insertOnStart
        public bool pr_test_attempt_insertOnStart(int testid)
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.pr_test_attempt_insertOnStart");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "Testid", DbType.Int32, testid);
                
                bool isSuccess = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                return isSuccess;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Result PR_Check_ResultByTestIDAndUserID
        public DataTable PR_Check_ResultByTestIDAndUserID()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Check_ResultByTestIDAndUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "TestID", DbType.Int32, CV.TestID());
                DataTable result = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    result.Load(dr);
                }
                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
