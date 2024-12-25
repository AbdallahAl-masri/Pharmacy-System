
namespace Infrastructure.Helper
{
    public interface IErrorLogService
    {
        void AddErrorLog(Exception ex, string moduleName);
    }
}
