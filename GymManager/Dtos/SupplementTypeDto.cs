﻿using System.ComponentModel.DataAnnotations;

namespace GymManager.Dtos
{
    public class SupplementTypeDto
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}