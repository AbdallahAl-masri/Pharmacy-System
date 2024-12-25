using EntitiyComponent.DBEntities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTO
{
    public class InvoiceDTO
    {
        public string ReferenceNumber { get; set; }
        //public int MedicineId { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string CustomerName { get; set; }

        public decimal GrandTotal { get; set; }

        public List<InvoiceDetailDTO> InvoiceDetails { get; set; }
    }
}
