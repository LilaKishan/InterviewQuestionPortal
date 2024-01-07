using InterviewQuestionPortal.Areas.QuestionType.Models;
using InterviewQuestionPortal.BAL;
using InterviewQuestionPortal.DAL.QuestionType;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InterviewQuestionPortal.Areas.QuestionType.Controllers
{
    [CheckAccess]
    [Area("QuestionType")]
    [Route("QuestionType/[controller]/[action]")]
    public class QuestionTypeController : Controller
    {
        QuestionTypeDALBase dalQuestionType = new QuestionTypeDALBase();

        #region method: QuestionTypeList
        public IActionResult QuestionTypeList()
        {
            DataTable dtsubject = dalQuestionType.dbo_PR_QuestionType_SelectAll();
            return View("QuestionTypeList", dtsubject);
        }
        #endregion

        #region method: AddQuestionType
        public IActionResult AddQuestionType(int? questionTypeID)
        {
            QuestionTypeModel questionTypeModel = dalQuestionType.dbo_PR_QuestionType_SelectByID(questionTypeID);
            if (questionTypeID != 0 && questionTypeID != null)
            {

                return View("AddQuestionType", questionTypeModel);
            }
            else
            {
                return View("AddQuestionType");
            }

        }
        #endregion

        #region Save
        public IActionResult Save(QuestionTypeModel questionTypeModel)
        {
             if (ModelState.IsValid)
            {
                if (dalQuestionType.dbo_QuestionType_Save(questionTypeModel))
                {
                    if (questionTypeModel.QuestionTypeID == 0 || questionTypeModel.QuestionTypeID==null)
                    {
                        TempData["questionTypeInsertMsg"] = "questionType Inserted Successfully";
                        return RedirectToAction("QuestionTypeList");
                    }
                    else
                    {
                        TempData["questionTypeupdateMsg"] = "questionType Updated Successfully";
                        return RedirectToAction("QuestionTypeList");
                    }
                }
            }
            return View("AddQuestionType");
        }

        #endregion

        #region method: Delete
        public IActionResult QuestionTypeDelete(int QuestionTypeID)
        {


            if (Convert.ToBoolean(dalQuestionType.dbo_PR_QuestionType_DeleteByID(QuestionTypeID)))
            {
                return RedirectToAction("QuestionTypeList");
            }

            return RedirectToAction("QuestionTypeList");


        }
        #endregion
    }
}
