
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RestaurantAddressRepository : EfEntityRepositoryBase<RestaurantAddress, ProjectDbContext>, IRestaurantAddressRepository
    {
        public RestaurantAddressRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
