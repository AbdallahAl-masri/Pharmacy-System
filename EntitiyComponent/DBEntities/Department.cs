﻿using System;
using System.Collections.Generic;

namespace EntitiyComponent.DBEntities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
