using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WhiteWebTech.API.Entities;

namespace WhiteWebTech.API.DataAccess;

public partial class WhiteDbContext : DbContext
{
    public WhiteDbContext()
    {
    }

    public WhiteDbContext(DbContextOptions<WhiteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<NewQuery> NewQueries { get; set; }

    
   
}
