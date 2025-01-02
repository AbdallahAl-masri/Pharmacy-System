namespace EntitiyComponent.DBEntities;

public partial class InvoiceDetail
{
    public int InvoiceDetailsId { get; set; }

    public int InvoiceMasterId { get; set; }

    public int MedicineId { get; set; }

    public int Qty { get; set; }

    public decimal TotalCost { get; set; }

    public decimal Price { get; set; }

    public virtual InvoiceMaster InvoiceMaster { get; set; }

    public virtual Medicine Medicine { get; set; }
}
