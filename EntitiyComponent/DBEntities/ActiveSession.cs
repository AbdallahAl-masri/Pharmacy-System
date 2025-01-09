using System;
using System.Collections.Generic;

namespace EntitiyComponent.DBEntities;

public partial class ActiveSession
{
    public int SessionId { get; set; }

    public string UserId { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Token { get; set; }
}
