using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab4.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Record>? Records { get; set; }
        [JsonIgnore]
        public virtual Account? Account { get; set; }

    }
}
