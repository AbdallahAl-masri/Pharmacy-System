using EntitiyComponent.DBEntities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTO
{
    public class InvoiceDTO
    {
        public string ReferenceNumber { get; set; }
        public int MedicineId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerName { get; set; }

        public List<InvoiceDetailDTO> InvoiceDetails { get; set; }
    }
}
