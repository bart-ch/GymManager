using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManager.Dtos
{
    public class MalfunctionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime MalfunctionDate { get; set; }
        public bool IsRepaired { get; set; }
        public EquipmentDto Equipment { get; set; }
        public int EquipmentId { get; set; }
    }
}