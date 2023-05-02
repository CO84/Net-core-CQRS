using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private  Guid GetUserId()
        {
            string userId = new(HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString());
            if(string.IsNullOrEmpty(userId)) 
                return Guid.Empty;
            else
            {
                return Guid.Parse(userId);
            }
            
        }
        public Guid UserId => GetUserId(); //new(HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
    }
}
