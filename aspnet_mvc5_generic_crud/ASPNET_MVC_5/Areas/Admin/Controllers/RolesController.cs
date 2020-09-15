using ASPNET_MVC_5.Areas.Admin.Models;
using System.Web.Mvc;

namespace ASPNET_MVC_5.Areas.Admin.Controllers
{
    [Authorize]
    public class RolesController : BaseCrudService<Role, RoleRepository> { }
}

