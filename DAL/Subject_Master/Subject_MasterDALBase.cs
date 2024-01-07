using InterviewQuestionPortal.Areas.Subject_Master.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace InterviewQuestionPortal.DAL.Subject_Master
{
    public class Subject_MasterDALBase:DALHelper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnectionString);

        #region Method: dbo_PR_Subject_Master_SelectAll
        public DataTable dbo_PR_Subject_Master_SelectAll()
        {
            try
            {
                //SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Subject_Master_selectall");

                DataTable dtSubject = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtSubject.Load(dr);
                }
                return dtSubject;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  Method : dbo.PR_Subject_Master_Insert & dbo.PR_Subject_Master_UpdateByid  name:dbo_PR_Subject_Master_Save
        public bool dbo_Subject_Master_Save(SubjectModel subjectModel)
        {
            try
            {
                Console.WriteLine(subjectModel.SubjectID);
                if (subjectModel.SubjectID == null || subjectModel.SubjectID == 0)
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.Pr_Subject_Master_Insert");
                    sqlDB.AddInParameter(dbCMD, "SubjectName", SqlDbType.VarChar,subjectModel.SubjectName);
                   sqlDB.AddInParameter(dbCMD, "@Created", DbType.DateTime, DBNull.Value);
                   sqlDB.AddInParameter(dbCMD, "@Modified", DbType.DateTime, DBNull.Value);

                    sqlDB.ExecuteNonQuery(dbCMD);
                    return true;
                }
                else
                {
                    DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Subject_Master_UpdateByid");
                    sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, subjectModel.SubjectID);
                    sqlDB.AddInParameter(dbCMD, "@SubjectName", DbType.String, subjectModel.SubjectName);
                    sqlDB.AddInParameter(dbCMD, "@UserID", DbType.Int32, subjectModel.UserID);
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


        #region Method : dbo.PR_Subject_Master_SelectByID
        public SubjectModel dbo_PR_Subject_Master_SelectByID(int SubjectID)
        {
            SubjectModel subjectModel = new SubjectModel();
            try
            {
                DbCommand dbCommand = sqlDB.GetStoredProcCommand("dbo.PR_Subject_Master_SelectByID");
                sqlDB.AddInParameter(dbCommand, "@SubjectID", DbType.Int32, SubjectID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    subjectModel.SubjectID = Convert.ToInt32(dataRow["SubjectID"]);
                    subjectModel.SubjectName = dataRow["SubjectName"].ToString();
                   // subjectModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    subjectModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                    subjectModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                }
                return subjectModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo.PR_Subject_Master_DeleteByID
        public bool? dbo_PR_Subject_Master_DeleteByID(int? subjectID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Subject_Master_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "@SubjectID", DbType.Int32, subjectID);

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
