using System;
using Microsoft.EntityFrameworkCore;

namespace ShrtLy.DAL
{
    public static class DatabaseInitializer
    {
        public static void ApplyMigrations<T>(string connectionString) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlServer(connectionString);
            var context = (T)Activator.CreateInstance(typeof(T), builder.Options);
            context?.Database.Migrate();
        }
    }
}
