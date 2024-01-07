using InterviewQuestionPortal.Areas.Question_Master.Models;
using InterviewQuestionPortal.BAL;
using InterviewQuestionPortal.DAL.MainTopic;
using InterviewQuestionPortal.DAL.Question_Master;
using InterviewQuestionPortal.DAL.QuestionType;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using InterviewQuestionPortal.DAL.SubTopic;

namespace InterviewQuestionPortal.Areas.Question_Master.Controllers
{
    [CheckAccess]
    [Area("Question_Master")]
    [Route("Question_Master/[controller]/[action]")]
    public class Question_MasterController : Controller
    {
        Question_MasterDALBase dalQuestion_Master = new Question_MasterDALBase();
        QuestionTypeDAL dalQ=new QuestionTypeDAL();
       SubTopicDAL dals = new SubTopicDAL();
        MainTopicDAL dalM = new MainTopicDAL();

        #region method: Question_MasterList
        public IActionResult Question_MasterList()
        {
            DataTable dtQuestion_Master = dalQuestion_Master.dbo_PR_Question_Master_SelectAll();
            return View("Question_MasterList", dtQuestion_Master);
        }
        #endregion

        #region method: Delete
        public IActionResult Question_MasterDelete(int QuestionID)
        {


            if (Convert.ToBoolean(dalQuestion_Master.dbo_PR_Question_Master_DeleteByID(QuestionID)))
            {
                return RedirectToAction("Question_MasterList");
            }

            return RedirectToAction("Question_MasterList");


        }
        #endregion

        #region method: AddQuestion_Master
        public IActionResult AddQuestion_Master(int QuestionID)
        {
            Question_MasterModel question_MasterModel = dalQuestion_Master.dbo_PR_Question_Master_SelectByID(QuestionID);
            if (QuestionID != 0 && QuestionID != null)
            {
                ViewBag.SubjectList = dalM.SubjectDropDown();
                ViewBag.MainTopicList = dals.MainTopicDropDown();
                ViewBag.SubTopicList = dals.SubTopicDropDown();
                ViewBag.QuestionTypeList = dalQ.QuestionTypeDropdown();
                return View("AddQuestion_Master", question_MasterModel);
            }
            else
            {
                ViewBag.SubjectList = dalM.SubjectDropDown();
                ViewBag.MainTopicList = dals.MainTopicDropDown();
                ViewBag.SubTopicList = dals.SubTopicDropDown();
                ViewBag.QuestionTypeList = dalQ.QuestionTypeDropdown();
                return View("AddQuestion_Master");
            }

        }
        #endregion

        #region Method: Save
        public IActionResult Save(Question_MasterModel question_MasterModel)
        {
            if (ModelState.IsValid)
            {
                if (dalQuestion_Master.dbo_PR_Question_MasterTopic_Save(question_MasterModel))
                {
                    if (question_MasterModel.QuestionID == 0 || question_MasterModel.QuestionID == null)
                    {
                        TempData["Question_MasterInsertMsg"] = "Question_Master Inserted Successfully";
                        return RedirectToAction("Question_MasterList");
                    }
                    else
                    {
                        TempData["Question_MasterupdateMsg"] = "Question_Master Updated Successfully";
                        return RedirectToAction("Question_MasterList");
                    }
                }
            }
            return View("AddQuestion_Master");
        }

        #endregion
    }
}
