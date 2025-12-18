using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreApi.Controllers
{
    public class LeaveController : Controller
    {
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}/approve")]
        public IActionResult ApproveLeave(int id)
        {
            return Ok("Approved");
        }

    }
}
