using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Don_SampleTrackingApp
{
    public class RaportareDbContext : DbContext
    {
        public RaportareDbContext(DbContextOptions<RaportareDbContext> options)
            : base(options)
        { }

        public DbSet<TrackingUser> Users { get; set; }
        public DbSet<ProbaModel> ProbaModels { get; set; }

    }
}
