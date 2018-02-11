using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bonbonniere.WebsiteVue.ApiControllers
{
    [Authorize]
    public class ApiBaseController : Controller
    {
    }
}
