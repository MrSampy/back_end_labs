namespace Lab3.Models
{
    public class Record: BaseModel
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
