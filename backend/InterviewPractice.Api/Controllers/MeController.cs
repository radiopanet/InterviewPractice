using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InterviewPractice.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MeController: ControllerBase
{
    [HttpGet]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        var email = User.FindFirstValue(ClaimTypes.Email)    
            ?? User.FindFirstValue("email");

        return Ok(new
        {
            Id = userId,
            Email = email,
            Claims = User.Claims.Select(c => new {c.Type, c.Value})
        });    
    }
}