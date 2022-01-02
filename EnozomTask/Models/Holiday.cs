using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnozomTask.Models
{
    public class Holiday
    {
        [Key]
        public int id { get; set; }
        public string HolidayName { get; set; }
        public string HolidayStartDate { get; set; }
        public string HolidayEndDate { get; set; }
        public virtual Country? Country { get; set; }
        [ForeignKey("countryid")]
        public int countryid { get; set; }

        
    }

}
