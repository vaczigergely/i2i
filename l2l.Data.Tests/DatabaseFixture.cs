using System;
using l2l.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace l2l.Data.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly L2lDbContextFactory factory;

        public DatabaseFixture()
        {
            factory = new L2lDbContextFactory();
            var db = GetNewL2lDbContext();

            if (factory.IsInMemoryDb())
            {
              db.Database.EnsureCreated();  
            }
            else
            {
                db.Database.Migrate();
            }       
        } 

        public L2lDbContext GetNewL2lDbContext()
        {
            return factory.CreateDbContext(new string[] {});
        }

        public void Dispose()
        {
            var db = GetNewL2lDbContext();
            factory.Dispose();  
            db.Database.EnsureDeleted();
            db.Dispose();
        }
    }
}