
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RestaurantSocialNetworkRepository : EfEntityRepositoryBase<RestaurantSocialNetwork, ProjectDbContext>, IRestaurantSocialNetworkRepository
    {
        public RestaurantSocialNetworkRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
