using PartialZ.Api.Dtos;

namespace PartialZ.Api.Services.Interfaces
{
    public interface IClaimService
    {
        Task<int> SaveClaimDetails(ClaimDto claimDto);
    }
}
