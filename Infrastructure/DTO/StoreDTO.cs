using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTO
{
    public class StoreDTO
    {
        public int StoreId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int SupplerId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int MedicineId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int OriginalQty { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int RemainingQty { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public decimal CostPrice { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public decimal TaxValue { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public decimal SellingPriceBeforeTax { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public decimal SellingPriceAfterTax { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public decimal MaxDiscount { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateOnly ProductionDate { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateOnly ExpiaryDate { get; set; }
    }
}
