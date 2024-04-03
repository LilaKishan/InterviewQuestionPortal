using InterviewQuestionPortal.Areas.MST_User.Models;
using InterviewQuestionPortal.Areas.Question_Master.Models;
using InterviewQuestionPortal.DAL.MST_User;
using InterviewQuestionPortal.DAL.Question_Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace InterviewQuestionPortal.Areas.MST_User.Controllers
{
    [Area("MST_User")]
    [Route("MST_User/[controller]/[action]")]
    public class MST_UserController : Controller
    {
        MST_UserDALBase DALMst_User = new MST_UserDALBase();
        public IActionResult MST_User_Dashboard()
        {
            return View();
        }

        #region Login
        public IActionResult Login_Page()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(MST_UserModel userLoginModel)
        {
            string ErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(userLoginModel.UserName))
            {
                ErrorMsg += "Username is required";
            }
            if (string.IsNullOrEmpty(userLoginModel.Password))
            {
                ErrorMsg += "<br/> Password is required";
            }

            if (ErrorMsg != string.Empty)
            {
                TempData["Error"] = ErrorMsg;

                return RedirectToAction("Login_Page", userLoginModel);
            }

            if (ModelState.IsValid)
            {


                DataTable dtLogin = DALMst_User.dbo_PR_MST_User_SelectByUserNamePassword(userLoginModel.UserName, userLoginModel.Password);

                if (dtLogin.Rows.Count == 0)
                {
                    TempData["Error"] = "Invalid Username or Password";
                    return RedirectToAction("Login_Page", "MST_User");
                }
                else
                {
                    //dtLogin.Load(objSDR);
                    if (dtLogin.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtLogin.Rows)
                        {
                            HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                            HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                            HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                            HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                            HttpContext.Session.SetString("ImageURL", dr["ImageURL"].ToString());
                            HttpContext.Session.SetString("Email", dr["Email"].ToString());
                            HttpContext.Session.SetString("Password", dr["Password"].ToString());
                            HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                            break;
                        }

                    }

                    if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("IsAdmin") == "True")
                    {
                        Console.WriteLine("Admin Login Success");
                        return RedirectToAction("Index", "Home");

                    }
                    else if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("IsAdmin") == "False")
                    {
                        return RedirectToAction("Index_User", "Home");
                    }

                    Console.WriteLine("Login Fail");

                    return RedirectToAction("Index", "Home");
                }


            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Logout
        //Logout action to clear current session and redirect user to login page
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region MST_User_Dashboard_Question list
        public IActionResult MST_User_Dashboard_Question()
        {

            Question_MasterDALBase dalQuestion_Master = new Question_MasterDALBase();
            DataTable dtQuestion_Master = dalQuestion_Master.dbo_PR_Question_Master_SelectAll();

            return View("MST_User_Dashboard_Question", dtQuestion_Master);
        }
        #endregion

        #region User_Account profilepage
        public IActionResult User_Account(int? UserID)
        {
            //DataTable userdata = DALMst_User.dbo_PR_MST_User_SelectByID(UserID);

            return View();
        }
        #endregion

        #region  Register
        public IActionResult MST_UserRegister()
        {
            return View();
        }
        public IActionResult Register(MST_UserModel mst_UserModel)
        {

            bool IsSuccess = DALMst_User.dbo_PR_MST_User_Register(mst_UserModel);
            Console.WriteLine(IsSuccess);
            Console.WriteLine(mst_UserModel.CoverPhoto);
            if (IsSuccess)
            {
                return RedirectToAction("Login_Page");
            }
            else
            {
                return RedirectToAction("MST_UserRegister");
            }
        }
        #endregion

        #region SelectAllUser
        public IActionResult MST_User_SelectAll()
        {
            DataTable dt = DALMst_User.PR_MST_USER_SELECTALL();
            return View(dt);
        }

        #endregion

        #region MST_User_DeleteByID
        public IActionResult MST_User_DeleteByID(int UserID)
        {
            bool isSuccess = DALMst_User.PR_MST_USER_DELETEBYID(UserID);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Data Deleted Successfully.";
                return RedirectToAction("MST_User_SelectAll");
            }
            return RedirectToAction("MST_User_SelectAll");
        }
        #endregion

        #region UpdateBYID
        //public IActionResult Save(MST_UserModel userModel)
        //{
        //    //DALMst_User.dbo_PR_MST_User_SelectByID(UserID);
        //    if (ModelState.IsValid)
        //    {
        //        if (DALMst_User.dbo_User_Master_UpdateByID(userModel))
        //        {
        //            if (userModel.UserID != null || userModel.UserID != 0)
        //            {
        //                TempData["AccountupdateMsg"] = "Account detail Updated Successfully";
        //                Console.WriteLine("Account detail Updated Successfully");
        //                return RedirectToAction("User_Account");

        //            }

        //            return RedirectToAction("User_Account");

        //        }
        //    }
        //    return View("User_Account");
        //}

        #endregion


    }
}
