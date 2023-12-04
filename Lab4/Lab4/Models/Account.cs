using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab4.Models
{
    public class Account: BaseModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Balance { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
