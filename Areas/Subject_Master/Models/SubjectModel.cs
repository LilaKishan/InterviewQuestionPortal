using System.ComponentModel.DataAnnotations;

namespace InterviewQuestionPortal.Areas.Subject_Master.Models
{
    public class SubjectModel
    {

        public int? SubjectID { get; set; } 
        //[Required]
        public string? SubjectName { get; set; } 
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public int? UserID { get; set; }
    }

    public class SubjectDropDownModel
    {
        public int? SubjectID { get; set; }
        public string? SubjectName { get; set; }
    }
}
