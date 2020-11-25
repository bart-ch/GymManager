using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class EquipmentOrder
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }

        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        [Range(typeof(DateTime), "01/01/2000", "01/01/2100")]
        public DateTime DesiredDeliveryDate { get; set; }

        public Type Type { get; set; }

        [Required]
        public byte TypeId { get; set; }

        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(typeof(int), "0", "10000")]
        public int Quantity { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [Required]
        public byte OrderStatusId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EquipmentOrder order &&
                   Id == order.Id &&
                   Brand == order.Brand &&
                   Model == order.Model &&
                   DesiredDeliveryDate == order.DesiredDeliveryDate &&
                   EqualityComparer<Type>.Default.Equals(Type, order.Type) &&
                   TypeId == order.TypeId &&
                   EqualityComparer<ApplicationUser>.Default.Equals(User, order.User) &&
                   UserId == order.UserId &&
                   Quantity == order.Quantity &&
                   EqualityComparer<OrderStatus>.Default.Equals(OrderStatus, order.OrderStatus) &&
                   OrderStatusId == order.OrderStatusId;
        }

        public override int GetHashCode()
        {
            int hashCode = -908846124;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Brand);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + DesiredDeliveryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + TypeId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ApplicationUser>.Default.GetHashCode(User);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserId);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<OrderStatus>.Default.GetHashCode(OrderStatus);
            hashCode = hashCode * -1521134295 + OrderStatusId.GetHashCode();
            return hashCode;
        }
    }
}