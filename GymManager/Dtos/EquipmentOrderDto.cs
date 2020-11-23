using GymManager.Core.Domain;
using System;

namespace GymManager.Dtos
{
    public class EquipmentOrderDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime DesiredDeliveryDate { get; set; }
        public TypeDto Type { get; set; }
        public byte TypeId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Quantity { get; set; }
        public OrderStatusDto OrderStatus { get; set; }
        public byte OrderStatusId { get; set; }
    }
}