using System.ComponentModel.DataAnnotations;

namespace InterviewQuestionPortal.Areas.Question_Master.Models
{
    public class Question_MasterModel
    {
        public int? QuestionID { get; set; }
        [Required]
        public string? Question { get; set; }
        public string? Option_A { get; set; }
        public string? Option_B { get; set; }
        public string? Option_C { get; set; }
        public string? Option_D { get; set; }
        public string? TrueOption { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? Description { get; set; }
        public int? QuestionTypeID { get; set; }
        public int? SubTopicID { get; set; }
        public int? MainTopicID { get; set; }
        public int? SubjectID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public int? UserID { get; set; }
    }
}
