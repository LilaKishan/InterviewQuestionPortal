using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InterviewQuestionPortal.Areas.MST_User.Models
{
    public class MST_UserModel
    {
        public int UserID { get; set; }
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string? ImageURL { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsAdmin { get;set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
      
        public IFormFile? CoverPhoto { get; set; }
    }
}
