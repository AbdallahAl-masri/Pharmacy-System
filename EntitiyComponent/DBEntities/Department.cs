namespace EntitiyComponent.DBEntities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
