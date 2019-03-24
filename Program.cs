using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Local
namespace EFCore.PG_847
{
    class Program
    {
        static string _connection;

        static void Main(string[] args)
        {
            _connection = $"Host={args[0]};Port={args[1]};Username={args[2]};Password={args[3]};Database=some_context;";

            using (var ctx = new SomeContext())
            {
                ctx.Database.EnsureCreated();

                var _ =
                    ctx.SomeDateTimes
                       .Where(s => s.Updated > DateTime.Now.AddDays(-1))
                       .ToArray();

                ctx.Database.EnsureDeleted();
            }

            Console.WriteLine("Finished!");
        }

        class SomeContext : DbContext
        {
            public DbSet<SomeDateTime> SomeDateTimes { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseNpgsql(_connection);
        }

        class SomeDateTime
        {
            public int Id { get; set; }

            public DateTime Updated { get; set; }
        }
    }
}
