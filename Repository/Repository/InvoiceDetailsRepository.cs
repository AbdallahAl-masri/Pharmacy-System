using EntitiyComponent.DBEntities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class InvoiceDetailsRepository : Repository<InvoiceDetail>, IInvoiceDetailsRepository
    {
    }
}
