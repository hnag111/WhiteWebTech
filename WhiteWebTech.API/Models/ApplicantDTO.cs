﻿using WhiteWebTech.API.Entities;

namespace WhiteWebTech.API.Models
{
    public class ApplicantDTO
    {
        public int? Id { get; set; }

        public int? JobId { get; set; }

        public string? ApplicantName { get; set; }

        public string? ApplicantDescription { get; set; }

        public int? ApplicantState { get; set; }

        public required IFormFile Cv { get; set; }

        public DateTime CreateDate { get; set; }

        
    }
}
