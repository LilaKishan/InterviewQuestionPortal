using InterviewQuestionPortal.BAL;
using InterviewQuestionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using NuGet.Protocol.Plugins;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace InterviewQuestionPortal.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger,IConfiguration _configuration)
        {
            configuration = _configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string ConnectionString = this.configuration.GetConnectionString("myConnectionString");
          DataTable dt=new DataTable();
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand objCmd = Conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[PR_Dashboard_Count]";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            Console.WriteLine(dt.Rows.Count);
            return View(dt);

        }

        public IActionResult Index_User()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}