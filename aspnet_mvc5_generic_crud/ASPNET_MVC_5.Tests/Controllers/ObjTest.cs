using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ASPNET_MVC_5.Models;
using ASPNET_MVC_5.Controllers;
using ASPNET_MVC_5.Areas.Admin.Controllers;
using ASPNET_MVC_5.Areas.Admin.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASPNET_MVC_5.Tests.Controllers
{
    [TestClass]
    class ObjTest
    {
        [TestMethod]
        public async void Index()
        {
            //RoleRepository _repo = new RoleRepository();
            ApplicationDbContext _context = new ApplicationDbContext();
        }


        
    }
}
