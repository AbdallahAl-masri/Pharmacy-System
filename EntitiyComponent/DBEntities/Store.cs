namespace EntitiyComponent.DBEntities;

public partial class Store
{
    public int StoreId { get; set; }

    public int SupplerId { get; set; }

    public int MedicineId { get; set; }

    public int OriginalQty { get; set; }

    public int RemainingQty { get; set; }

    public decimal CostPrice { get; set; }

    public decimal TaxValue { get; set; }

    public decimal SellingPriceBeforeTax { get; set; }

    public decimal SellingPriceAfterTax { get; set; }

    public decimal MaxDiscount { get; set; }

    public DateOnly ProductionDate { get; set; }

    public DateOnly ExpiaryDate { get; set; }

    public virtual Medicine Medicine { get; set; }

    public virtual Supplier Suppler { get; set; }
}
