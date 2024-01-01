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
        SubTopicDALBase dalSubTopic= new SubTopicDALBase();
        SubTopicDAL dals = new SubTopicDAL();

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
    }
}
