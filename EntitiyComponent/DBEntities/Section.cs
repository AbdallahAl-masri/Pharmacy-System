using System;
using System.Collections.Generic;

namespace EntitiyComponent.DBEntities;

public partial class Section
{
    public int SectionId { get; set; }

    public string SectionName { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; }
}
