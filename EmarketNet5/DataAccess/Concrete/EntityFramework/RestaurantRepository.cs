
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RestaurantRepository : EfEntityRepositoryBase<Restaurant, ProjectDbContext>, IRestaurantRepository
    {
        public RestaurantRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
