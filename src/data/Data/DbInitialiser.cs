using ChatNetCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbInitialiser
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ChatDbContext chatDbContext;

        public DbInitialiser(ApplicationDbContext applicationDbContext, ChatDbContext chatDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.chatDbContext = chatDbContext;
        }

        public void Run()
        {
            this.applicationDbContext.Database.EnsureDeleted();
            this.chatDbContext.Database.EnsureDeleted();

            this.applicationDbContext.Database.EnsureCreated();
            this.chatDbContext.Database.EnsureCreated();
        }
    }
}
