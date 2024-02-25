namespace InterviewQuestionPortal.Areas.MST_Test.Models
{
    public class TestQuestionModel
    {
        public int TestQuestionID { get; set; }
        public string? Question { get; set; }
        public string? Option_A { get; set; }
        public string? Option_B { get; set; }
        public string? Option_C { get; set; }
        public string? Option_D { get; set; }
        public string? TrueOption { get; set; }
        public string? CorrectAnswer { get; set; }
        public int? UserID { get; set; }
    }
}
