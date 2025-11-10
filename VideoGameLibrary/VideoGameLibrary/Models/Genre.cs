using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; }
    }
}
