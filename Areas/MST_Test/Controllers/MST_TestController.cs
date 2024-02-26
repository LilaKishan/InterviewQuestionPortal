using InterviewQuestionPortal.Areas.MST_Test.Models;
using InterviewQuestionPortal.Areas.Question_Master.Models;
using InterviewQuestionPortal.BAL;
using InterviewQuestionPortal.DAL.MainTopic;
using InterviewQuestionPortal.DAL.MST_Test;
using InterviewQuestionPortal.DAL.Question_Master;
using InterviewQuestionPortal.DAL.QuestionType;
using InterviewQuestionPortal.DAL.SubTopic;
using InterviewQuestionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InterviewQuestionPortal.Areas.MST_Test.Controllers
{
    [CheckAccess]
    [Area("MST_Test")]
    [Route("MST_Test/[controller]/[action]")]
    public class MST_TestController : Controller
    {
        MST_TestDALBase dalMST_Test = new MST_TestDALBase();
        SubTopicDAL dals = new SubTopicDAL();
        MainTopicDAL dalM = new MainTopicDAL();

        public IActionResult Index()
        {
            return View();
        }

        #region Test Genereate
        public IActionResult Test_GenerationByUserPage(int SubjectID,int MainTopicID)
        {
            ViewBag.SubjectList = dalM.SubjectDropDown();
            ViewBag.MainTopicList = dals.MainTopicDropDown(SubjectID);
            ViewBag.SubTopicList = dals.SubTopicDropDown(MainTopicID);

            return View();
        }

        #endregion

        public IActionResult TestPage()
        {
            DataTable testQuestionModel = dalMST_Test.dbo_PR_TestQuestion_selectByUserID();
            ViewModel viewModel = new ViewModel()
            {
                TestQuestion=testQuestionModel,
            };

            return View("TestPage",viewModel);
        }

        #region Method: Save
        public IActionResult Save(MST_TestModel mST_TestModel)
        {
            string message = "";
            try {
                if (ModelState.IsValid)
                {
                    if (dalMST_Test.dbo_PR_Generate_Test_Save(mST_TestModel))
                    {
                        //TempData["TestGenerateMsg"] = "Test Generate Successfully";
                        return RedirectToAction("TestPage");
                    }
                    else
                    {
                        message = "Not Enough Question, please enter less number of question ";
                        TempData["Message"] = message;
                        return RedirectToAction("Test_GenerationByUserPage");
                    }
                }
                
            }
            catch(Exception ex) {
               // message = "Not Enough Question, please enter less number of question: " + ex.Message;
            }
            //ViewBag.Message = message;
            return View("Test_GenerationByUserPage");
        }

        #endregion

        #region method: maintopicDropDown
        public IActionResult DropDownMainTopic(int SubjectID)
        {
            var maintopicDD = dals.MainTopicDropDown(SubjectID);
            return Json(maintopicDD);
        }

        #endregion

        #region method: SubtopicDropDown
        public IActionResult DropDownSubTopic(int MainTopicID)
        {
            var subtopicDD = dals.SubTopicDropDown(MainTopicID);
            return Json(subtopicDD);
        }

        #endregion
    }
}
