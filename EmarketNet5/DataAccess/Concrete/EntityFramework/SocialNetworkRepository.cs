
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SocialNetworkRepository : EfEntityRepositoryBase<SocialNetwork, ProjectDbContext>, ISocialNetworkRepository
    {
        public SocialNetworkRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
