#nullable disable

namespace DataAccess.Entities
{
    public class Genre : Record
    {
        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
