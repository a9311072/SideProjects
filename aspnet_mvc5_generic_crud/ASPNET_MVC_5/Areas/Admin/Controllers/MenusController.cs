using ASPNET_MVC_5.Areas.Admin.Models;
using ASPNET_MVC_5.Areas.Admin.Repositories;
using ASPNET_MVC_5.Areas.Admin.Services;
using System.Web.Mvc;

namespace ASPNET_MVC_5.Areas.Admin.Controllers
{

    [Authorize]
    public class MenusController : BaseCrudService<Menu, MenuRepository> { }

}
