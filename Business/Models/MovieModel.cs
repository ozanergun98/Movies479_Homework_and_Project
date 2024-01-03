#nullable disable

using DataAccess.Entities;

namespace Business.Models
{
    public class MovieModel : Record
    {

        public string Name { get; set; }

        public short? Year { get; set; }

        public float Revenue { get; set; }

        public int? DirectorId { get; set; }

        //public string DirectorOutput { get; set; }
    }
}
