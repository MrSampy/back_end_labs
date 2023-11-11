using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab3.Models
{
    public class Record: BaseModel
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
