using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PartialZ.Api.Dtos;
using PartialZ.Api.Services.Interfaces;

namespace PartialZ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private IEmployee _employee;
        public EmployeeController(IEmployee employee) { 
        this._employee = employee;
        }
        [HttpGet]
        public async Task<IActionResult> Employee(string EmailID)
        {
          var result= await this._employee.GetEmployee(EmailID);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> RegsregisterEmployee(RegistrationDto registrationDto)
        {
            var result = await this._employee.RegsregisterEmployee(registrationDto.Email, registrationDto.Password);
            return Json(result);
        }
        [HttpGet]
        [Route("VerifyEmployee")]
        public async Task<IActionResult> VerifyEmployee(string token)
        {
            var result = await this._employee.VerifyEmployee(token);
            return Ok(result);
        }

        [HttpGet]
        [Route("EmployeeDetails")]
        public List<RegistrationDetailsDto> GetEmployeeDetails(string EmailID)
        {
            var result = this._employee.GetEmployeeDetails(EmailID);
            return result;
        }
        [HttpGet]
        [Route("GetFilerNames")]
        public List<FilerDto> GetFilerNames()
        {
            var result = this._employee.GetFilerNames();
            return result;
        }
    }
}
