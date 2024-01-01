using InterviewQuestionPortal.Areas.MST_User.Models;
using InterviewQuestionPortal.DAL.MST_User;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InterviewQuestionPortal.Areas.MST_User.Controllers
{

    [Area("MST_User")]
    [Route("MST_User/[controller]/[action]")]
    public class MST_UserController : Controller
    {
        
        #region Configuration

        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MST_UserController(IConfiguration _Configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = _Configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion
        //MST_UserDALBase DALMst_User = new MST_UserDALBase(_webHostEnvironment);
       // MST_UserDAL Mst_UserDAL = new MST_UserDAL(_webHostEnvironment);
        public IActionResult Login_Page()
        {
            return View();
        }

        #region Login
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
                


                MST_UserDALBase DALMst_User = new MST_UserDALBase(_webHostEnvironment);
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
                          
                            break;
                        }

                    }
                   
                    if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                    {
                        Console.WriteLine("Login Success");
                        return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Login_Page", "MST_User");
        }
        #endregion

        public IActionResult MST_UserRegister()
        {
            return View();
        }

        #region  Register
        public IActionResult Register(MST_UserModel mst_UserModel)
        {
            MST_UserDALBase DALMst_User = new MST_UserDALBase(_webHostEnvironment);
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
    }
}
