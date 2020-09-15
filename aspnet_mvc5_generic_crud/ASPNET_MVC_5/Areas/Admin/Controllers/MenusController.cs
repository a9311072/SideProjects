using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPNET_MVC_5.Areas.Admin.Models;
using ASPNET_MVC_5.Models;

namespace ASPNET_MVC_5.Areas.Admin.Controllers
{
    public class MenuRepository : BaseRepository<Menu> { }

    [Authorize]
    public class MenusController : BaseCrudService<Menu, MenuRepository> { }

}
