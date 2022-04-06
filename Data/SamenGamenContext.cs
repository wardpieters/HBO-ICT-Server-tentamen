using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamenGamen.Models;

namespace SamenGamen.Data
{
    public class SamenGamenContext : DbContext
    {
        public SamenGamenContext (DbContextOptions<SamenGamenContext> options)
            : base(options)
        {
        }

        public DbSet<SamenGamen.Models.Deelnemer> Deelnemer { get; set; }

        public DbSet<SamenGamen.Models.Gamevoorstel> Gamevoorstel { get; set; }

        public DbSet<SamenGamen.Models.Aanmelding> Aanmelding { get; set; }
    }
}
