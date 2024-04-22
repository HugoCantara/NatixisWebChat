namespace NatixisWebChatModels.AppModels
{
    using System.ComponentModel.DataAnnotations;

    public class StockCodeModel
    {
        [Key]
        public int Id { get; set; }

        public string? Country { get; set; }

        public string? StockCode { get; set; }

        public decimal StockValue { get; set; }
    }
}