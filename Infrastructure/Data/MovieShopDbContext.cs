using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext: DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options ): base(options)
        {

        }
    }
}
