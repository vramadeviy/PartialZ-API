using PartialZ.Api.Dtos;
namespace PartialZ.Api.Services.Interfaces
{
    public interface IEmployeeDirectoryService
    {
      List<StateDto> GetStates();
        DropdownsDto GetAllDropdowns();
        Task<int> SaveEmplpyeeDirectoryDetails(EmployeeDirectoryDto empDirectoryDto);
        Task<int> BulkSaveEmplpyeeDirectoryDetails(List<EmployeeDirectoryListDto> empDirectoryDto);
        List<EmployeeDirectoryDetailsDto> GetEmployeeDirectoryDetails(string EmailID);
        Task<int> DeleteEmplpyeeDirectoryDetails(string ssn);
    }
}
