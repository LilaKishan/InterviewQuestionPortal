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
using System.Configuration;
using System.Data;
using System.Timers;

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
        public IActionResult Test_GenerationByUserPage(int SubjectID, int MainTopicID)
        {
            ViewBag.SubjectList = dalM.SubjectDropDown();
            ViewBag.MainTopicList = dals.MainTopicDropDown(SubjectID);
            ViewBag.SubTopicList = dals.SubTopicDropDown(MainTopicID);

            return View();
        }

        #endregion

        #region TestPage
        public IActionResult TestPage(int testid)
        {

            ViewData["TestID"] = testid;

            var testAttemptQuestion = dalMST_Test.pr_test_attempt_insertOnStart(testid);
            DataTable testQuestionModel = dalMST_Test.PR_Test_Attempt_SelectQuestionByUserIDAndTestID(testid);

            ViewModel viewModel = new ViewModel()
            {
                TestQuestion = testQuestionModel,
            };
            //SetTimer(30);
            return View("TestPage", viewModel);
        }
        #endregion

        #region Method: Save
        public IActionResult Save(MST_TestModel mST_TestModel)
        {
            string message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    int flag = dalMST_Test.dbo_PR_Generate_Test_Save(mST_TestModel);
                    Console.WriteLine("TestID : " + flag);
                    if (flag != -1)
                    {
                        //TempData["TestGenerateMsg"] = "Test Generate Successfully";
                        return RedirectToAction("TestList", new { testId = flag });
                    }
                    else
                    {
                        message = "Not Enough Question, please enter less number of question ";
                        TempData["Message"] = message;
                        return RedirectToAction("Test_GenerationByUserPage");
                    }
                }

            }
            catch (Exception ex)
            {

            }
            //ViewBag.Message = message;
            return View("Test_GenerationByUserPage");
        }

        #endregion

        #region Method : TestList
        public IActionResult TestList()
        {
            DataTable generateTestModel = dalMST_Test.dbo_PR_Generate_Test_Selectall();
            foreach (DataRow dr in generateTestModel.Rows)
            {
                HttpContext.Session.SetString("TestID", dr["TestID"].ToString());
                break;
            }
            ViewModel viewModel = new ViewModel()
            {
                Generate_Test = generateTestModel,
            };

            return View("TestList", viewModel);

        }
        #endregion

        #region ResultPage
        public IActionResult ResultPage()
        {
            DataTable result = dalMST_Test.PR_Check_ResultByTestIDAndUserID();
            return View(result);
        }
        #endregion

        //#region Timer

        //private static System.Timers.Timer aTimer;

        //public static void Main()
        //{
        //    SetTimer(30);

        //    Console.WriteLine("\nPress the Enter key to exit the application...\n");
        //    Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
        //    Console.ReadLine();
        //    aTimer.Stop();
        //    aTimer.Dispose();

        //    Console.WriteLine("Terminating the application...");
        //}

        //private static void SetTimer(int duration)
        //{
        //    // Create a timer with a two second interval.

        //    aTimer = new System.Timers.Timer(duration / 6000);
        //    // Hook up the Elapsed event for the timer. 
        //    aTimer.Elapsed += OnTimedEvent;
        //    aTimer.AutoReset = false;
        //    aTimer.Enabled = true;
        //}

        //private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
        //                      e.SignalTime);
        //}
        //#endregion

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

        #region method: UpdateTrueOption
        public IActionResult UpdateTrueOption(int testID, int testQuestionID, string trueoption)
        {
            Console.WriteLine("Test ID :" + testID);
            bool isSuccess = dalMST_Test.PR_Test_Attempt_UpdateByTestIDAndUserID(testID, testQuestionID, trueoption);
            Console.WriteLine("isSuccess :" + isSuccess);
            return Json(new { success = isSuccess });
        }

        #endregion
    }


}

