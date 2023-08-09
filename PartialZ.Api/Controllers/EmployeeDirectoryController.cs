using Microsoft.AspNetCore.Mvc;
using PartialZ.Api.Dtos;
using PartialZ.Api.Services.Interfaces;

namespace PartialZ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDirectoryController : Controller
    {
        private IEmployeeDirectoryService _employeedirectoryservice;

        public EmployeeDirectoryController(IEmployeeDirectoryService employeedirectoryservice)
        {
            this._employeedirectoryservice = employeedirectoryservice;
        }
        [HttpGet] 
        public List<StateDto> GetStates()
        {
            var result = this._employeedirectoryservice.GetStates();
            return result;
        }

        [HttpGet]
        [Route("DropDownList")]
        public DropdownsDto GetAllDropdowns()
        {
            var result = this._employeedirectoryservice.GetAllDropdowns();
            return result;
        }
        [HttpPost]
        [Route("SaveEmployeeDirectory")]
        public async Task<IActionResult> SaveEmplpyeeDirectoryDetails(EmployeeDirectoryDto empDirectoryDto)
        {
            var result = await this._employeedirectoryservice.SaveEmplpyeeDirectoryDetails(empDirectoryDto);
            return Ok(result);
        }
        [HttpGet]
        [Route("EmployeeDirectoryDetails")]
        public List<EmployeeDirectoryDetailsDto> GetEmployeeDetails(string EmailID)
        {
            var result = this._employeedirectoryservice.GetEmployeeDirectoryDetails(EmailID);
            return result;
        }

        [HttpGet]
        [Route("EmployeeDirectoryClaimantDetails")]
        public EmployeeDirectoryDto GetEmployeedirectoryClaimantdetails(string ssn)
        {
            var result = this._employeedirectoryservice.GetEmployeedirectoryClaimantdetails(ssn);
            return result;
        }
    }
}
