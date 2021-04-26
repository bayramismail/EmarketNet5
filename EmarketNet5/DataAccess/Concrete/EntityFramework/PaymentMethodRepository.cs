
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PaymentMethodRepository : EfEntityRepositoryBase<PaymentMethod, ProjectDbContext>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
