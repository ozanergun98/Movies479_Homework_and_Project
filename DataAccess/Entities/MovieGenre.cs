#nullable disable

using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities
{
    [PrimaryKey(nameof(MovieId), nameof(GenreId))]
    public class MovieGenre
    {
        //[Key]
        //[Column(Order = 0)]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        //[Key]
        //[Column(Order = 1)]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
