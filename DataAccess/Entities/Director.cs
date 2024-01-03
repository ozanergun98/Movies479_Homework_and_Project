#nullable disable

namespace DataAccess.Entities
{
    public class Director : Record
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool IsRetired { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
