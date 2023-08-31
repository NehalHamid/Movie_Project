using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie_Project.Models
{
    public class User_Movie
    {
        [Key]
        public int Id { get; set; }

        public int? movieId { get; set; }
        [ForeignKey("movie_id")]
        public Movie? movie { get; set; }

        public int? userId { get; set; }
        [ForeignKey("user_id")]
        public User? actor { get; set; }
    }
}
