using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static TastyDelivery.Areas.Admin.AdminConstants;

namespace TastyDelivery.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdminRoleName)]
    public class AdminController : Controller
    {
    }
}
