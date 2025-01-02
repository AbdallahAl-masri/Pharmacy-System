namespace EntitiyComponent.DBEntities;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; }

    public int MedicineDepartmentId { get; set; }

    public string Description { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual MedicineDepartment MedicineDepartment { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
