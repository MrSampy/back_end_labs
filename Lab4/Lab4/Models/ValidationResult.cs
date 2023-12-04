using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Messages { get; set; }

        public override string ToString()
        {
            return IsValid ? "Validation succeed" : "Validation failed: " + string.Join(", ", Messages);
        }
    }
}
