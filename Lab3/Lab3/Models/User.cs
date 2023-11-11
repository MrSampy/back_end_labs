using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab3.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Record>? Records { get; set; }
        [JsonIgnore]
        public virtual Account? Account { get; set; }

    }
}
