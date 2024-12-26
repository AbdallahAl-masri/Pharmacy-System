namespace Infrastructure.DTO
{
    public class InvoiceDetailDTO
    {
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
