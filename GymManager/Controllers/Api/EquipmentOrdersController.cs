using AutoMapper;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using GymManager.Attributes;
using System;
using Microsoft.AspNet.Identity;

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
                .GetEquipmentOrdersWithTypesAndOrderStatuses()
                .Select(Mapper.Map<EquipmentOrder, EquipmentOrderDto>);

            return Ok(equipmentOrderDtos);
        }

        public IHttpActionResult GetEquipmentOrder(int id)
        {
            var equipmentOrder = unitOfWork.EquipmentOrders
                .GetSingleEquipmentOrderWithTypeAndOrderStatus(eo => eo.Id == id);

            if (equipmentOrder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EquipmentOrder, EquipmentOrderDto>(equipmentOrder));
        }

        [HttpPost]
    //    [ValidateAntiForgeryToken]
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
    }
}
