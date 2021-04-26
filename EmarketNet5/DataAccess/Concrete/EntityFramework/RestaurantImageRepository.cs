
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RestaurantImageRepository : EfEntityRepositoryBase<RestaurantImage, ProjectDbContext>, IRestaurantImageRepository
    {
        public RestaurantImageRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
