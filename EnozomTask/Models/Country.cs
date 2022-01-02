using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnozomTask.Models
{
    public class Country
    {
        [Key]
        public int id { get; set; }
        public string CountryName { get; set; }
        public string CountryCca2 { get; set; }
    }
}
