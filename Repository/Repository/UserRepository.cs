using EntitiyComponent.DBEntities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
    }
}
