#nullable disable

using DataAccess.Entities;

namespace Business.Models
{
    public class DirectorModel : Record
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool IsRetired { get; set; }

        public string DirectorOutput { get; set; }
    }
}
