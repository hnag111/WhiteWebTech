using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhiteWebTech.API.Entities;

public partial class Applicant
{
    [Key]
    public int Id { get; set; }

    public int? JobId { get; set; }

    public string? ApplicantName { get; set; }

    public string? ApplicantDescription { get; set; }

    public int? ApplicantState { get; set; }

    public  byte[]? Cv { get; set; }
    public required string filename { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual Job? Job { get; set; }
}
