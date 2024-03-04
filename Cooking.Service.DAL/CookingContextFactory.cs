using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Service.DAL
{
    public class CookingContextFactory : IDesignTimeDbContextFactory<CookingDbContext>
    {
        //Just setting the design time context here
        public CookingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CookingDbContext>();
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=Cooking;Integrated Security=True;");

            return new CookingDbContext(optionsBuilder.Options);
        }
    }
}
