using System;
using System.Collections.Generic;

namespace EntitiyComponent.DBEntities;

public partial class Module
{
    public int ModuleId { get; set; }

    public string ModuleName { get; set; }

    public string MuduleUrl { get; set; }

    public virtual ICollection<ModuleRole> ModuleRoles { get; set; } = new List<ModuleRole>();
}
