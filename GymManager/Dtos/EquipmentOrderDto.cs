using GymManager.Core.Domain;
using System;
using System.Collections.Generic;

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
        public ApplicationUserDto User { get; set; }
        public int Quantity { get; set; }
        public OrderStatusDto OrderStatus { get; set; }
        public byte OrderStatusId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EquipmentOrderDto dto &&
                   Id == dto.Id &&
                   Brand == dto.Brand &&
                   Model == dto.Model &&
                   DesiredDeliveryDate == dto.DesiredDeliveryDate &&
                   EqualityComparer<TypeDto>.Default.Equals(Type, dto.Type) &&
                   TypeId == dto.TypeId &&
                   UserId == dto.UserId &&
                   EqualityComparer<ApplicationUserDto>.Default.Equals(User, dto.User) &&
                   Quantity == dto.Quantity &&
                   EqualityComparer<OrderStatusDto>.Default.Equals(OrderStatus, dto.OrderStatus) &&
                   OrderStatusId == dto.OrderStatusId;
        }

        public override int GetHashCode()
        {
            int hashCode = -1004827236;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Brand);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + DesiredDeliveryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TypeDto>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + TypeId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserId);
            hashCode = hashCode * -1521134295 + EqualityComparer<ApplicationUserDto>.Default.GetHashCode(User);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<OrderStatusDto>.Default.GetHashCode(OrderStatus);
            hashCode = hashCode * -1521134295 + OrderStatusId.GetHashCode();
            return hashCode;
        }
    }
}