using InterviewQuestionPortal.Areas.Subject_Master.Models;
using InterviewQuestionPortal.BAL;
using InterviewQuestionPortal.DAL.Subject_Master;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InterviewQuestionPortal.Areas.Subject_Master.Controllers
{
    [CheckAccess]
    [Area("Subject_Master")]
    [Route("Subject_Master/[controller]/[action]")]
    public class SubjectController : Controller
    {
     

        Subject_MasterDALBase dalSubject_Master=new Subject_MasterDALBase();

        #region method: SubjectList
        public IActionResult SubjectList()
        {
            DataTable dtsubject=dalSubject_Master.dbo_PR_Subject_Master_SelectAll();
            return View("SubjectList",dtsubject);
        }
        #endregion

        #region method: AddSubject
        public IActionResult AddSubject(int? subjectID)
        {
            SubjectModel subjectModel = dalSubject_Master.dbo_PR_Subject_Master_SelectByID(subjectID);
            if (subjectID != 0 && subjectID!=null)
            {
                
                return View("AddSubject", subjectModel);
            }
            else
            {
                return View("AddSubject");
            }
            
        }
        #endregion

        #region Save
        public IActionResult Save(SubjectModel subjectModel)
        {
          // if (ModelState.IsValid)
            {
                if (dalSubject_Master.dbo_Subject_Master_Save(subjectModel))
                {
                    if (subjectModel.SubjectID== 0)
                    {
                        TempData["SubjectInsertMsg"] = "Subject Inserted Successfully";
                        return RedirectToAction("SubjectList");
                    }
                    else
                    {
                        TempData["SubjectupdateMsg"] = "Subject Updated Successfully";
                        return RedirectToAction("SubjectList");
                    }
                }
            }
            return View("AddSubject");
        }

        #endregion

        #region method: Delete
        public IActionResult SubjectDelete(int SubjectID)
        {
          

            if (Convert.ToBoolean(dalSubject_Master.dbo_PR_Subject_Master_DeleteByID(SubjectID)))
            {
                return RedirectToAction("SubjectList");
            }

            return RedirectToAction("SubjectList");


        }
        #endregion
    }
}
