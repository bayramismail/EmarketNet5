
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class WorkingHourRepository : EfEntityRepositoryBase<WorkingHour, ProjectDbContext>, IWorkingHourRepository
    {
        public WorkingHourRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
