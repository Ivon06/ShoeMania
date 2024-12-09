using Microsoft.EntityFrameworkCore;
using ShoeMania.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ShoeMania.Tests.Mock
{
    public class DbMock
    {
        public static ShoeManiaDbContext Instance
        {
            get
            {

                var options = new DbContextOptionsBuilder<ShoeManiaDbContext>()
                        .UseInMemoryDatabase("ShoeManiaInMemoryDb" + Guid.NewGuid().ToString())
                        .Options;

                return new ShoeManiaDbContext(options, false);
            }
        }
    }
}
