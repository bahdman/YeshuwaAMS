namespace src.Models
{
    public class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Amount { get; set; }
        public string TranRef { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
