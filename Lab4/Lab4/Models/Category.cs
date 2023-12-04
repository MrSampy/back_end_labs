using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab4.Models
{
    public class Category : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Record>? Records { get; set; }
    }
}
