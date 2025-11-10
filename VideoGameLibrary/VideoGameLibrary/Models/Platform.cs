using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Models
{
    public class Platform
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}
