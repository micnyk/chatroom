using System;
using System.Linq;
using ChatRoom.Domain;
using ChatRoom.Domain.Entities.Rooms;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Web.Extensions
{
    public static class DatabaseInitializer
    {
        public static IApplicationBuilder DatabaseMigrateSeed(this IApplicationBuilder appBuilder)
        {
            using (var dbContext = new DesignTimeDbContextFactory().CreateDbContext(new string[0]))
            {
                dbContext.Database.Migrate();

                if (!dbContext.Rooms.Any())
                {
                    dbContext.Rooms.Add(new Room
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "General"
                    });

                    dbContext.Rooms.Add(new Room
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "IT"
                    });
                }

                dbContext.SaveChanges();
            }

            return appBuilder;
        }
    }
}