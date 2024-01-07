using System.ComponentModel.DataAnnotations;

namespace InterviewQuestionPortal.Areas.QuestionType.Models
{
    public class QuestionTypeModel
    {
       
            public int? QuestionTypeID { get; set; }
            [Required]
            public string? QuestionType { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? Modified { get; set; }
            public int? UserID { get; set; }
        
    }
    public class QuestionTypeDropdownModel
    {
        public int QuestionTypeID { get; set; }
        public string? QuestionType { get; set; }
    }
}
