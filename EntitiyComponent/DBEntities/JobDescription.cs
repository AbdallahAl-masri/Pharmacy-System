namespace EntitiyComponent.DBEntities;

public partial class JobDescription
{
    public int JobDescriptonId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
