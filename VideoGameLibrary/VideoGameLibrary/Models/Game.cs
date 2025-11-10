
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Models


{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public ICollection<GameGenre>? GameGenres { get; set; }
        public ICollection<GamePlatform>? GamePlatforms { get; set; }
    }
}
