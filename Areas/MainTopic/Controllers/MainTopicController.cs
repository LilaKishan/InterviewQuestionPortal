using InterviewQuestionPortal.Areas.MainTopic.Models;
using InterviewQuestionPortal.Areas.Subject_Master.Models;
using InterviewQuestionPortal.BAL;
using InterviewQuestionPortal.DAL.MainTopic;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InterviewQuestionPortal.Areas.MainTopic.Controllers
{
    [CheckAccess]
    [Area("MainTopic")]
    [Route("MainTopic/[controller]/[action]")]
    public class MainTopicController : Controller
    {
        MainTopicDALBase dalMainTopic = new MainTopicDALBase();
        MainTopicDAL dalM = new MainTopicDAL();

        #region method: MainTopicList
        public IActionResult MainTopicList()
        {
            DataTable dtMainTopic = dalMainTopic.dbo_PR_MainTopic_SelectAll();
            return View("MainTopicList", dtMainTopic);
        }
        #endregion

        #region method: Delete
        public IActionResult MainTopicDelete(int MainTopicID)
        {


            if (Convert.ToBoolean(dalMainTopic.dbo_PR_MainTopic_DeleteByID(MainTopicID)))
            {
                return RedirectToAction("MainTopicList");
            }

            return RedirectToAction("MainTopicList");


        }
        #endregion

        #region method: AddMainTopic
        public IActionResult AddMainTopic(int MainTopicID)
        {
            MainTopicModel mainTopicModel= dalMainTopic.dbo_PR_MainTopic_SelectByID(MainTopicID);
            if (MainTopicID != 0 && MainTopicID != null)
            {
                ViewBag.SubjectList = dalM.SubjectDropDown();
                return View("AddMainTopic", mainTopicModel);
            }
            else
            {
                ViewBag.SubjectList = dalM.SubjectDropDown();
                return View("AddMainTopic");
            }

        }
        #endregion

        #region Method: Save
        public IActionResult Save(MainTopicModel mainTopicModel)
        {
            //if (ModelState.IsValid)
            {
                if (dalMainTopic.dbo_PR_MainTopic_Save(mainTopicModel))
                {
                    if (mainTopicModel.MainTopicID == 0)
                    {
                        TempData["MainTopicInsertMsg"] = "MainTopic Inserted Successfully";
                        return RedirectToAction("MainTopicList");
                    }
                    else
                    {
                        TempData["MainTopicupdateMsg"] = "MainTopic Updated Successfully";
                        return RedirectToAction("MainTopicList");
                    }
                }
            }
            return View("AddMainTopic");
        }

        #endregion
    }
}
