using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RoleClaimsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        [HttpGet("role")]
        public IActionResult GetUserByRole()
        {
            // Simulate a logged-in user with a role
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "mock"));


            HttpContext.User = user;

            // Perform role-based authorization manually
            if (User.IsInRole("Admin"))
            {
                return Ok(new { Message = "Access granted for the Admin role." });
            }

            else
            {
                return Forbid();


            }
        }

        [HttpGet("claim")]
        public IActionResult GetUserByClaim()
        {         // Simulate a logged-in user with a claim
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim("Department", "IT")
            }, "mock"));

            HttpContext.User = user;

            var hasClaim = user.HasClaim(c => c.Type == "Department" && c.Value == "IT");

            // Perform claim-based authorization manually
            if (hasClaim)
            {
                return Ok(new { Message = "Access granted for the IT Department." });
            }
            else
            {
                return Forbid();
            }
        }
    }
}
    
