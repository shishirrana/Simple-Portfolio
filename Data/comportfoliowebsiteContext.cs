using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using com.portfolio.website.Models;

namespace com.portfolio.website.Data
{
    public class comportfoliowebsiteContext : DbContext
    {
        public comportfoliowebsiteContext (DbContextOptions<comportfoliowebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<com.portfolio.website.Models.PersonalInformation> PersonalInformation { get; set; } = default!;

        public DbSet<com.portfolio.website.Models.MyExpertise>? MyExpertise { get; set; }

        public DbSet<com.portfolio.website.Models.Education>? Education { get; set; }
        public DbSet<com.portfolio.website.Models.User>? User { get; set; }
        public DbSet<com.portfolio.website.Models.Skill>? Skill { get; set; }
        public DbSet<com.portfolio.website.Models.Language>? Language { get; set; }

    }
}
