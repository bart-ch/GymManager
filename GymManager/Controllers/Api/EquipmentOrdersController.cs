using AutoMapper;
using GymManager.Attributes;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class EquipmentOrdersController : ApiController
    {

        private readonly IUnitOfWork unitOfWork;

        public EquipmentOrdersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetEquipmentOrders()
        {
            var equipmentOrderDtos = unitOfWork.EquipmentOrders
                .GetEquipmentOrdersWithUsersAndTypesAndOrderStatuses()
                .Select(Mapper.Map<EquipmentOrder, EquipmentOrderDto>);

            return Ok(equipmentOrderDtos);
        }

        public IHttpActionResult GetEquipmentOrder(int id)
        {
            var equipmentOrder = unitOfWork.EquipmentOrders
                .GetEquipmentOrderWithTypeAndOrderStatus(eo => eo.Id == id);

            if (equipmentOrder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EquipmentOrder, EquipmentOrderDto>(equipmentOrder));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHttpActionResult CreateEquipmentOrder(EquipmentOrderDto equipmentOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            equipmentOrderDto.UserId = User.Identity.GetUserId();
            equipmentOrderDto.OrderStatusId = OrderStatus.InProgressId;

            var equipmentOrder = Mapper.Map<EquipmentOrderDto, EquipmentOrder>(equipmentOrderDto);

            unitOfWork.EquipmentOrders.Add(equipmentOrder);
            unitOfWork.Complete();

            equipmentOrderDto.Id = equipmentOrder.Id;

            return Created(new Uri(Request.RequestUri + "/" + equipmentOrder.Id), equipmentOrderDto);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IHttpActionResult UpdateEquipmentOrder(int id, EquipmentOrderDto equipmentOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var equipmentOrderInDb = unitOfWork.EquipmentOrders.SingleOrDefault(eo => eo.Id == id);
            if (equipmentOrderInDb == null)
            {
                return NotFound();
            }

            equipmentOrderInDb.Brand = equipmentOrderDto.Brand;
            equipmentOrderInDb.Model = equipmentOrderDto.Model;
            equipmentOrderInDb.TypeId = equipmentOrderDto.TypeId;
            equipmentOrderInDb.DesiredDeliveryDate = equipmentOrderDto.DesiredDeliveryDate;
            equipmentOrderInDb.Quantity = equipmentOrderDto.Quantity;

            unitOfWork.Complete();

            return Ok();
        }

        [HttpPut]
        [Route("api/equipmentOrders/{id}/{orderStatusId}")]
        public IHttpActionResult UpdateOrderStatusOfEquipment(int id, byte orderStatusId)
        {
            var orderStatusInDb = unitOfWork.OrderStatuses.SingleOrDefault(os => os.Id == orderStatusId);
            if (orderStatusInDb == null)
            {
                return BadRequest();
            }

            var equipmentOrderInDb = unitOfWork.EquipmentOrders.SingleOrDefault(eo => eo.Id == id);
            if (equipmentOrderInDb == null)
            {
                return NotFound();
            }

            equipmentOrderInDb.OrderStatusId = orderStatusId;
            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteEquipmentOrder(int id)
        {
            var equipmentOrderInDb = unitOfWork.EquipmentOrders.SingleOrDefault(eo => eo.Id == id);

            if (equipmentOrderInDb == null)
            {
                return NotFound();
            }

            unitOfWork.EquipmentOrders.Remove(equipmentOrderInDb);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
