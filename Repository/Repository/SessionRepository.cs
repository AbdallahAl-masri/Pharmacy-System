using EntitiyComponent.DBEntities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class sessionRepository : Repository<ActiveSession>, ISessionRepository
    {
    }
}
