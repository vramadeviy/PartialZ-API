using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartialZ.Api.Dtos;
using PartialZ.Api.Services.Interfaces;
using PartialZ.DataAccess.PartialZDB;
namespace PartialZ.Api.Services
{
    public class ClaimService : IClaimService
    {
        private PartialClaimsContext _PartialClaimsContext;
        public ClaimService(PartialClaimsContext PartialClaimsContext)
        {
            this._PartialClaimsContext = PartialClaimsContext;
        }

       public async Task<int> SaveClaimDetails(ClaimDto claimDto)
        {
            try
            {
                int claimID = 0;
                if (this._PartialClaimsContext.Claims.Where(e => e.SocialSecurityNumber == claimDto.SocialSecurityNumber).Any())
                {
                    //update
                    var existingdata = await this._PartialClaimsContext.Claims.Where(e => e.SocialSecurityNumber == claimDto.SocialSecurityNumber).FirstAsync();
                    existingdata.SocialSecurityNumber = claimDto.SocialSecurityNumber;
                    existingdata.DateOfBirth = claimDto.DateOfBirth;
                    existingdata.TelephoneNumber = claimDto.TelephoneNumber;
                    existingdata.ClaimantFirstName = claimDto.ClaimantFirstName;
                    existingdata.ClaimantMiddleName = claimDto.ClaimantMiddleName;
                    existingdata.ClaimantSuffix = claimDto.ClaimantSuffix;
                    existingdata.ClaimantLastName = claimDto.ClaimantLastName;

                    existingdata.MailingStreetAddress = claimDto.MailingStreetAddress;
                    existingdata.MailingCity = claimDto.MailingCity;
                    existingdata.MailingState = claimDto.MailingState;
                    existingdata.ZipCode = claimDto.ZipCode;
                    existingdata.Gender = claimDto.Gender;
                    existingdata.Handicap = claimDto.Handicap;
                    existingdata.VeteranStatus = claimDto.VeteranStatus;

                    existingdata.Race = claimDto.Race;
                    existingdata.Ethnicity = claimDto.Ethnicity;
                    existingdata.FederalwithHolding = claimDto.FederalwithHolding;
                    existingdata.Citizen = claimDto.Citizen;
                    existingdata.AuthorizedAlienNumber = claimDto.AuthorizedAlienNumber;
                    existingdata.Education = claimDto.Gender;
                    existingdata.Occupation = claimDto.Occupation;

                    existingdata.WeekEndingDate = claimDto.WeekEndingDate;
                    existingdata.LastDateWorked = claimDto.LastDateWorked;
                    existingdata.Earnings = claimDto.Earnings;
                    existingdata.VacationPay = claimDto.VacationPay;
                    existingdata.HolidayPay = claimDto.HolidayPay;
                    existingdata.OtherPay = claimDto.OtherPay;
                    existingdata.OtherStateWages = claimDto.OtherStateWages;


                    existingdata.LastModifedDate = DateTime.UtcNow;
                    await this._PartialClaimsContext.SaveChangesAsync();
                    claimID = existingdata.ClaimId;
                }
                else
                {
                    //insert
                    var data = new Claim()
                    {
                        SocialSecurityNumber = claimDto.SocialSecurityNumber,
                        DateOfBirth = claimDto.DateOfBirth,
                        TelephoneNumber = claimDto.TelephoneNumber,
                        ClaimantFirstName = claimDto.ClaimantFirstName,
                        ClaimantMiddleName = claimDto.ClaimantMiddleName,
                        ClaimantSuffix = claimDto.ClaimantSuffix,
                        ClaimantLastName = claimDto.ClaimantLastName,
                        MailingStreetAddress = claimDto.MailingStreetAddress,
                        MailingCity = claimDto.MailingCity,
                        MailingState = claimDto.MailingState,
                        ZipCode = claimDto.ZipCode,
                        Gender = claimDto.Gender,
                        Handicap = claimDto.Handicap,
                        VeteranStatus = claimDto.VeteranStatus,
                        Race = claimDto.Race,
                        Ethnicity = claimDto.Ethnicity,
                        FederalwithHolding = claimDto.FederalwithHolding,
                        Citizen = claimDto.Citizen,
                        AuthorizedAlienNumber = claimDto.AuthorizedAlienNumber,
                        Education = claimDto.Education,
                        Occupation = claimDto.Occupation,
                        CreatedDate = DateTime.UtcNow,
                        WeekEndingDate = claimDto.WeekEndingDate,
                    LastDateWorked = claimDto.LastDateWorked,
                    Earnings = claimDto.Earnings,
                    VacationPay = claimDto.VacationPay,
                    HolidayPay = claimDto.HolidayPay,
                    OtherPay = claimDto.OtherPay,
                    OtherStateWages = claimDto.OtherStateWages
                };
                    await this._PartialClaimsContext.Claims.AddAsync(data);
                    await this._PartialClaimsContext.SaveChangesAsync();
                    claimID = data.ClaimId;
                }
                return claimID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
