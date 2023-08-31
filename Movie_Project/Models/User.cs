using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie_Project.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Name Required")]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Name Required")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email  Required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "not match")]
        public string ConfirmPassword { get; set; }
        public string? status { get; set; }
        public ICollection<User_Movie>? user_Movies { get; set; }
 
     
    }
}
