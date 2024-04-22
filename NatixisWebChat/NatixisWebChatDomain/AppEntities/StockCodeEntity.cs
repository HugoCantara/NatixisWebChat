namespace NatixisWebChatDomain.AppEntities
{
    using System.ComponentModel.DataAnnotations;

    public class StockCodeEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }

        [MaxLength(10)]
        public string? StockCode { get; set; }

        public decimal StockValue { get; set; }
    }
}