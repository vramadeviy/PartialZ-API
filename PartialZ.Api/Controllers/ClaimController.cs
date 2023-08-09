using Microsoft.AspNetCore.Mvc;
using PartialZ.Api.Dtos;
using PartialZ.Api.Services.Interfaces;

namespace PartialZ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : Controller
    {
        private IClaimService _claimservice;

        [HttpPost]
        [Route("SaveClaim")]
        public async Task<IActionResult> SaveClaimDetails(ClaimDto claimDto)
        {
            var result = await this._claimservice.SaveClaimDetails(claimDto);
            return Ok(result);
        }
    }
}
