
namespace Service.Interfaces
{
    public interface IErrorLogService
    {
        void AddErrorLog(Exception ex, string moduleName);
    }
}
