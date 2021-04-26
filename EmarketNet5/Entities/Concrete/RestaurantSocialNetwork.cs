using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class RestaurantSocialNetwork : IEntity
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int SocialNetworkId { get; set; }
        public string Path { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}