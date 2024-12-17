namespace EntitiyComponent.DBEntities;

public partial class MedicineDepartment
{
    public int MedicineDepartmentId { get; set; }

    public string DepartmentName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
