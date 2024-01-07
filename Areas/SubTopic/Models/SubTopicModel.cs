using System.ComponentModel.DataAnnotations;

namespace InterviewQuestionPortal.Areas.SubTopic.Models
{
    public class SubTopicModel
    {
        public int? SubTopicID { get; set; }
        [Required]
        public string? SubTopicName { get; set; }
        public int? MainTopicID { get; set; }
        public int? SubjectID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public int? UserID { get; set; }
    }
    public class SubTopicDropDownModel
    {
        public int SubTopicID { get; set; }
        public string? SubTopicName { get; set; }
    }
}
