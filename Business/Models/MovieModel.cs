#nullable disable

using System.ComponentModel;
using DataAccess.Entities;

namespace Business.Models
{
    public class MovieModel : Record
    {

        public string Name { get; set; }

        public short? Year { get; set; }

		[DisplayName("Revenue($M)")]
		public float Revenue { get; set; }

        [DisplayName("Director")]
        public int? DirectorId { get; set; }

		[DisplayName("Director")]
		public string? DirectorOutput { get; set; }
    }
}
