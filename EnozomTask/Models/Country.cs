using System.ComponentModel.DataAnnotations;


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
