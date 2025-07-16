using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string FeeName { get; set; }
        public int Amount { get; set; }
        public int Installments { get; set; }
    }
}
