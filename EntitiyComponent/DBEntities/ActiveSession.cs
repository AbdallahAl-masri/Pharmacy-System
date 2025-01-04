using System;
using System.Collections.Generic;

namespace EntitiyComponent.DBEntities;

public partial class ActiveSession
{
    public string UserId { get; set; }

    public string SessionId { get; set; }

    public DateTime ExpiryDate { get; set; }
}
