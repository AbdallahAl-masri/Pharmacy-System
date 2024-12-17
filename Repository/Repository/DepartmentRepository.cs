using EntitiyComponent.DBEntities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Repository.Repository
{
    public class DepartmentRepository : Repository<Department>, IRepository.IDepartmentRepository
    {
        public DepartmentRepository()
        {

        }

    }
}
