namespace EntitiyComponent.DBEntities;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; }

    public virtual ICollection<AssignUsersToRole> AssignUsersToRoles { get; set; } = new List<AssignUsersToRole>();

    public virtual ICollection<ModuleRole> ModuleRoles { get; set; } = new List<ModuleRole>();
}
