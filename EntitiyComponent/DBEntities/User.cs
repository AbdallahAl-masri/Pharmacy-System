namespace EntitiyComponent.DBEntities;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string Mobilenumber { get; set; }

    public bool Gender { get; set; }

    public string Address { get; set; }

    public bool ShiftType { get; set; }

    public int Salary { get; set; }

    public int JobDescriptionId { get; set; }

    public DateOnly JoinDate { get; set; }

    public DateOnly? ResignationDate { get; set; }

    public bool IsActive { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public int? DeparmentId { get; set; }

    public int? SectionId { get; set; }

    public virtual ICollection<AssignUsersToRole> AssignUsersToRoles { get; set; } = new List<AssignUsersToRole>();

    public virtual Department Deparment { get; set; }

    public virtual ICollection<ErrorLog> ErrorLogs { get; set; } = new List<ErrorLog>();

    public virtual JobDescription JobDescription { get; set; }

    public virtual Section Section { get; set; }
}
