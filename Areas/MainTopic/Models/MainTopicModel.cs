using System.ComponentModel.DataAnnotations;

namespace InterviewQuestionPortal.Areas.MainTopic.Models
{
    public class MainTopicModel
    {
        public int? MainTopicID { get; set; } 
        [Required]
        public string? MainTopicName { get;set; }
        [Required]
        public int? SubjectID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public int? UserID { get; set; }

    }

    public class MainTopicDropDownModel
    {
        public int MainTopicID { get; set; }
        public string MainTopicName { get; set; }
    }
}
