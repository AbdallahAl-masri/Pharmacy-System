using EntitiyComponent.DBEntities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository()
        {

        }

    }
}
