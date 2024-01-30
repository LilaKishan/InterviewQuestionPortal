namespace InterviewQuestionPortal.BAL
{
    public class CV
    {
        private static IHttpContextAccessor _httpContextAccessor;

        static CV()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        public static String? UserName()
        {
            string? UserName = null;

            if (_httpContextAccessor.HttpContext.Session.GetString("UserName") != null)
            {
                UserName = _httpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
            }
            return UserName;
        }
        public static int? UserID()
        {
            int? UserID = null;

            if (_httpContextAccessor.HttpContext.Session.GetString("UserID") != null)
            {
                UserID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID").ToString());
            }
            return UserID;
        }
        public static string? ImageURL()
        {
            string? ImageURL = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("ImageURL") != null)
            {
                ImageURL =
               _httpContextAccessor.HttpContext.Session.GetString("ImageURL").ToString();
            }

            return ImageURL;
        }

    }
}
