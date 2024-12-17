using EntitiyComponent.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
