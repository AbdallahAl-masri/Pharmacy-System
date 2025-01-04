using EntitiyComponent.DBEntities;
using Repository.IRepository;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorLogService(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }
        public void AddErrorLog(Exception ex, string moduleName)
        {
            try
            {

                var errorLog = new ErrorLog
                {
                    ErrorExeption = ex.InnerException?.ToString() ?? "",
                    ErrorMessage = ex.Message ?? "",
                    ModuleName = moduleName,
                    TransactionDate = DateTime.Now
                };

                _errorLogRepository.Add(errorLog);
            }
            catch (Exception ex1)
            {
                try
                {
                    // Define log file path (adjust path as needed for your environment)
                    string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLogs", "ApplicationErrors.log");

                    // Ensure directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                    // Append error to log file
                    File.AppendAllText(logFilePath,
                        $"[{DateTime.Now}] {ex1.Message}{Environment.NewLine}{ex1.StackTrace}{Environment.NewLine}");
                }
                catch
                {
                    // As a last resort, write to console to ensure the error is visible in development
                    Console.Error.WriteLine($"Critical error logging failure: {ex1.Message}");
                }
            }
        }
    }
}
