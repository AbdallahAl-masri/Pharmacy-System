namespace EntitiyComponent.DBEntities;

public partial class ModuleRole
{
    public int ModuleRoleId { get; set; }

    public int RoleId { get; set; }

    public int ModuleId { get; set; }

    public virtual Module Module { get; set; }

    public virtual Role Role { get; set; }
}
