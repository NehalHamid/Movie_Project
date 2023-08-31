using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie_Project.Models
{
    public class Gener
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "A Genre is required")]
        public string genre { get; set; }

        public ICollection<Movie>? movies { get; set; }

       
    }
}
