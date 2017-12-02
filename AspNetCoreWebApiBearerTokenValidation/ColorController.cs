using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiBearerTokenValidation
{
    [Authorize]
    public class ColorController : Controller
    {
        [HttpGet("~/api/colors")]
        public Task<List<string>> GetColors() => Task.FromResult(new List<string>() { "Red", "Yellow", "Blue" });
    }
}
