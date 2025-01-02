using Infrastructure.DTO;

namespace Service.Interfaces
{
    public interface IStoreService
    {
        public Task<HttpResponseMessage> GetAllSupplier();
        public Task<HttpResponseMessage> GetAllMedicine();
        public Task<HttpResponseMessage> AddNewStore(StoreDTO storeDTO);
        public Task<HttpResponseMessage> GetAllStores();
        public Task<HttpResponseMessage> GetStoreById(int Id);
        public Task<HttpResponseMessage> Delete(int Id);
        public Task<HttpResponseMessage> UpdateStore(StoreDTO storeDTO);
    }
}
