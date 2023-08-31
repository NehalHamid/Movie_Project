using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie_Project.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "A Movie Title is required")]
        public string Title { get; set; }
        public string Image { get; set; }
        public string actors { get; set; }
        public string FilmProducer { get; set; }
        public string? Description { get; set; }
        public int YearOfRelease { get; set; }
        [Range(1.0, 10.0,
            ErrorMessage = "Rating must be between1.0 and 10.0")]
        public float rating { get; set; }
        public string Duartion { get; set; }
        public string WatchLink { get; set; }
        public string DownloadeLink { get; set; }
        public string? GenerName { get; set; }   

        public int? GenerId { get; set; }
        [ForeignKey("GenerId")]
        public Gener? gener { get; set; }

        public ICollection<User_Movie>? user_Movies { get; set; }
    }
}
