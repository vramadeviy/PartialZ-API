using PartialZ.Api.Dtos;
namespace PartialZ.Api.Services.Interfaces
{
    public interface IEmployeeDirectoryService
    {
      List<StateDto> GetStates();
        DropdownsDto GetAllDropdowns();
        Task<int> SaveEmplpyeeDirectoryDetails(EmployeeDirectoryDto empDirectoryDto);
        List<EmployeeDirectoryDetailsDto> GetEmployeeDirectoryDetails(string EmailID);
        EmployeeDirectoryDto GetEmployeedirectoryClaimantdetails(string ssn);
        Task<string> DeleteEmplpyeeDirectoryDetails(string ssn);
    }
}
