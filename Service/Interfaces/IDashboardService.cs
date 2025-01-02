namespace Service.Interfaces
{
    public interface IDashboardService
    {
        public Task<HttpResponseMessage> GetDashboardDetails();
    }
}
