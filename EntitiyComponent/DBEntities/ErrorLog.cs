namespace EntitiyComponent.DBEntities;

public partial class ErrorLog
{
    public int ErrorId { get; set; }

    public string ErrorMessage { get; set; }

    public string ErrorExeption { get; set; }

    public string ModuleName { get; set; }

    public DateTime TransactionDate { get; set; }

    public int? UserId { get; set; }

    public virtual User User { get; set; }
}
