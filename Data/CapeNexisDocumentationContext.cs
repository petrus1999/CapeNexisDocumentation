using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapeNexisDocumentation.Models;

namespace CapeNexisDocumentation.Data
{
    public class CapeNexisDocumentationContext : DbContext
    {
        public CapeNexisDocumentationContext (DbContextOptions<CapeNexisDocumentationContext> options)
            : base(options)
        {
        }

        public DbSet<CapeNexisDocumentation.Models.Learners> Learner { get; set; } = default!;

        public DbSet<CapeNexisDocumentation.Models.facilitators> facilitator { get; set; } = default!;

        public DbSet<CapeNexisDocumentation.Models.courses> Course { get; set; } = default!;
    }
}
