using EntitiyComponent.DBEntities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTO
{
    public class InvoiceDTO
    {
        public int InvoiceMasterId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string ReferenceNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateOnly TransactionDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string CustomerName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? NumberOfItems { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        //public decimal? TotalCostPrice { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public decimal? TotalSellingPrice { get; set; }

        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
