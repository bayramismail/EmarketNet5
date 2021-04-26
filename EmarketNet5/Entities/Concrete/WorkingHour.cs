using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class WorkingHour : IEntity
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
