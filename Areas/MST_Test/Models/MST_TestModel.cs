using System.ComponentModel.DataAnnotations;

namespace InterviewQuestionPortal.Areas.MST_Test.Models
{
    public class MST_TestModel
    {
        public int? TestID { get; set; }
        public int? UserID { get; set; }
        public int? SubjectID { get; set; }
        public int? MainTopicID { get; set; }
        public int? SubTopicID { get; set; }
        [Required]
        public int? Duration { get; set; }
        [Required]
        public int? Total_Questions { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }

    
   
}
