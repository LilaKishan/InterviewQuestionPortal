using InterviewQuestionPortal.Areas.MainTopic.Models;
using InterviewQuestionPortal.Areas.SubTopic.Models;
using InterviewQuestionPortal.BAL;
using InterviewQuestionPortal.DAL.MainTopic;
using InterviewQuestionPortal.DAL.SubTopic;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InterviewQuestionPortal.Areas.SubTopic.Controllers
{
    [CheckAccess]
    [Area("SubTopic")]
    [Route("SubTopic/[controller]/[action]")]
    public class SubTopicController : Controller
    {
        SubTopicDALBase dalSubTopic = new SubTopicDALBase();
        SubTopicDAL dals = new SubTopicDAL();
        MainTopicDAL dalM = new MainTopicDAL();

        #region method: SubTopicList
        public IActionResult SubTopicList()
        {
            DataTable dtSubTopic = dalSubTopic.dbo_PR_SubTopic_SelectAll();
            return View("SubTopicList", dtSubTopic);
        }
        #endregion

        #region method: Delete
        public IActionResult SubTopicDelete(int SubTopicID)
        {


            if (Convert.ToBoolean(dalSubTopic.dbo_PR_Subtopic_DeleteByID(SubTopicID)))
            {
                return RedirectToAction("SubTopicList");
            }

            return RedirectToAction("SubTopicList");


        }
        #endregion

        #region method: AddSubTopic
        public IActionResult AddSubTopic(int SubTopicID, int SubjectID)
        {
            SubTopicModel subTopicModel = dalSubTopic.dbo_PR_SubTopic_SelectByID(SubTopicID);
            if (SubTopicID != 0 && SubTopicID != null)
            {
                ViewBag.SubjectList = dalM.SubjectDropDown();
                ViewBag.MainTopicList = dals.MainTopicDropDown(SubjectID);
                return View("AddSubTopic",subTopicModel);
            }
            else
            {
                ViewBag.SubjectList = dalM.SubjectDropDown();
                ViewBag.MainTopicList = dals.MainTopicDropDown(SubjectID);
                return View("AddSubTopic");
            }

        }
        #endregion

        #region Method: Save
        public IActionResult Save(SubTopicModel subTopicModel)
        {
            if (ModelState.IsValid)
            {
                if (dalSubTopic.dbo_PR_SubTopicTopic_Save(subTopicModel))
                {
                    if (subTopicModel.SubTopicID == 0 || subTopicModel.SubTopicID == null)
                    {
                        TempData["SubTopicInsertMsg"] = "SubTopic Inserted Successfully";
                        return RedirectToAction("SubTopicList");
                    }
                    else
                    {
                        TempData["SubTopicupdateMsg"] = "SubTopic Updated Successfully";
                        return RedirectToAction("SubTopicList");
                    }
                }
            }
            return View("AddMainTopic");
        }

        #endregion

        #region method: subtopicDropDown
        public IActionResult DropDownSubTopic()
        {
            var subtopicDD = dals.SubTopicDropDown();
            return Json(subtopicDD);
        }

        #endregion

        #region method: maintopicDropDown
        public IActionResult DropDownMainTopic(int SubjectID)
        {
            var maintopicDD = dals.MainTopicDropDown(SubjectID);
            return Json(maintopicDD);
        }

        #endregion
    }
}
