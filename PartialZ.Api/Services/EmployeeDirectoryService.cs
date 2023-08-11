using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PartialZ.Api.Dtos;
using PartialZ.Api.Services.Interfaces;
using PartialZ.DataAccess.PartialZDB;
namespace PartialZ.Api.Services
{
    public class EmployeeDirectoryService : IEmployeeDirectoryService
    {
        private PartialClaimsContext _PartialClaimsContext;
        public EmployeeDirectoryService(PartialClaimsContext PartialClaimsContext)
        {
            this._PartialClaimsContext = PartialClaimsContext;
        }
        public List<StateDto> GetStates()
        {
            return this._PartialClaimsContext.States.Select(e => new StateDto
            {
                StateId = e.StateId,
                StateCode = e.StateCode
            }).ToList();
        }
        public DropdownsDto GetAllDropdowns()
        {
            DropdownsDto dd = new DropdownsDto();

            dd.raceDto = this._PartialClaimsContext.DropdownRaces.Select(e => new RaceDto
            {
                RaceId = e.RaceId,
                Code = e.Code
            }).ToList();
            dd.citizenDto = this._PartialClaimsContext.DropdownCitizens.Select(e => new CitizenDto
            {
                CitizenId = e.CitizenId,
                Code = e.Code
            }).ToList();
            dd.educationDto = this._PartialClaimsContext.DropdownEducations.Select(e => new EducationDto
            {
                EducationId = e.EducationId,
                Code = e.Code
            }).ToList();
            dd.ethnicityDto = this._PartialClaimsContext.DropdownEthnicities.Select(e => new EthnicityDto
            {
                EthnicityId = e.EthnicityId,
                Code = e.Code
            }).ToList();

            dd.genderDto = this._PartialClaimsContext.DropdownGenders.Select(e => new GenderDto
            {
                GenderId = e.GenderId,
                Code = e.Code
            }).ToList();
            dd.handicapDto = this._PartialClaimsContext.DropdownHandicaps.Select(e => new HandicapDto
            {
                HandicapId = e.HandicapId,
                Code = e.Code
            }).ToList();
            dd.nameSuffixDto = this._PartialClaimsContext.DropdownNameSuffixes.Select(e => new NameSuffixDto
            {
                NameSuffixId = e.NameSuffixId,
                Code = e.Code
            }).ToList();
            dd.otherWageDto = this._PartialClaimsContext.DropdownOtherWages.Select(e => new OtherWageDto
            {
                OtherWagesId = e.OtherWagesId,
                Code = e.Code
            }).ToList();
            dd.veteranDto = this._PartialClaimsContext.DropdownVeterans.Select(e => new VeteranDto
            {
                VeteranId = e.VeteranId,
                Code = e.Code
            }).ToList();
            dd.withholdingDto = this._PartialClaimsContext.DropdownWithholdings.Select(e => new WithholdingDto
            {
                WithholdingsId = e.WithholdingsId,
                Code = e.Code
            }).ToList();



            return dd;



        }

        public async Task<int> SaveEmplpyeeDirectoryDetails(EmployeeDirectoryDto empDirectoryDto)
        {
            try
            {
                int employeeDirectoryID = 0;
                if (this._PartialClaimsContext.EmployeeDirectories.Where(e => e.SocialSecurityNumber == empDirectoryDto.SocialSecurityNumber).Any())
                {
                    //update
                    var existingdata = await this._PartialClaimsContext.EmployeeDirectories.Where(e => e.SocialSecurityNumber == empDirectoryDto.SocialSecurityNumber).FirstAsync();
                    existingdata.SocialSecurityNumber = empDirectoryDto.SocialSecurityNumber;
                    existingdata.DateOfBirth = empDirectoryDto.DateOfBirth;
                    existingdata.TelephoneNumber = empDirectoryDto.TelephoneNumber;
                    existingdata.ClaimantFirstName = empDirectoryDto.ClaimantFirstName;
                    existingdata.ClaimantMiddleName = empDirectoryDto.ClaimantMiddleName;
                    existingdata.ClaimantSuffix = empDirectoryDto.ClaimantSuffix;
                    existingdata.ClaimantLastName = empDirectoryDto.ClaimantLastName;

                    existingdata.MailingStreetAddress = empDirectoryDto.MailingStreetAddress;
                    existingdata.MailingCity = empDirectoryDto.MailingCity;
                    existingdata.MailingState = empDirectoryDto.MailingState;
                    existingdata.ZipCode = empDirectoryDto.ZipCode;
                    existingdata.Gender = empDirectoryDto.Gender;
                    existingdata.Handicap = empDirectoryDto.Handicap;
                    existingdata.VeteranStatus = empDirectoryDto.VeteranStatus;

                    existingdata.Race = empDirectoryDto.Race;
                    existingdata.Ethnicity = empDirectoryDto.Ethnicity;
                    existingdata.FederalwithHolding = empDirectoryDto.FederalwithHolding;
                    existingdata.Citizen = empDirectoryDto.Citizen;
                    existingdata.AuthorizedAlienNumber = empDirectoryDto.AuthorizedAlienNumber;
                    existingdata.Education = empDirectoryDto.Education;
                    existingdata.Occupation = empDirectoryDto.Occupation;


                    existingdata.LastModifedDate = DateTime.UtcNow;
                    await this._PartialClaimsContext.SaveChangesAsync();
                    employeeDirectoryID = existingdata.EmployeeDirectoryId;
                }
                else
                {
                    var filerid = await this._PartialClaimsContext.Filers.Where(e => e.Email == empDirectoryDto.Email).Select(x => x.EmployeeId).FirstAsync();
                    //insert
                    var data = new EmployeeDirectory()
                    {
                        SocialSecurityNumber = empDirectoryDto.SocialSecurityNumber,
                        FilerId = filerid,
                        DateOfBirth = empDirectoryDto.DateOfBirth,
                        TelephoneNumber = empDirectoryDto.TelephoneNumber,
                        ClaimantFirstName = empDirectoryDto.ClaimantFirstName,
                        ClaimantMiddleName = empDirectoryDto.ClaimantMiddleName,
                        ClaimantSuffix = empDirectoryDto.ClaimantSuffix,
                        ClaimantLastName = empDirectoryDto.ClaimantLastName,
                        MailingStreetAddress = empDirectoryDto.MailingStreetAddress,
                        MailingCity = empDirectoryDto.MailingCity,
                        MailingState = empDirectoryDto.MailingState,
                        ZipCode = empDirectoryDto.ZipCode,
                        Gender = empDirectoryDto.Gender,
                        Handicap = empDirectoryDto.Handicap,
                        VeteranStatus = empDirectoryDto.VeteranStatus,
                        Race = empDirectoryDto.Race,
                        Ethnicity = empDirectoryDto.Ethnicity,
                        FederalwithHolding = empDirectoryDto.FederalwithHolding,
                        Citizen = empDirectoryDto.Citizen,
                        AuthorizedAlienNumber = empDirectoryDto.AuthorizedAlienNumber,
                        Education = empDirectoryDto.Education,
                        Occupation = empDirectoryDto.Occupation,
                        CreatedDate = DateTime.UtcNow
                    };
                    await this._PartialClaimsContext.EmployeeDirectories.AddAsync(data);
                    await this._PartialClaimsContext.SaveChangesAsync();
                    employeeDirectoryID = data.EmployeeDirectoryId;
                }
                return employeeDirectoryID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmployeeDirectoryDetailsDto> GetEmployeeDirectoryDetails(string EmailID)
        {
            var result = this._PartialClaimsContext.EmployeeDirectories
              .Join(this._PartialClaimsContext.Filers,
              f => f.FilerId, s => s.EmployeeId,
              (f, s) => new { f, s })
                .Join(this._PartialClaimsContext.DropdownGenders, e => e.f.Gender, x => x.GenderId,
                (e, x) => new { e, x })
                .Join(this._PartialClaimsContext.States, e => e.e.f.MailingState, x => x.StateId,
               (z, h) => new EmployeeDirectoryDetailsDto
               {
                   Email = z.e.s.Email,
                   SocialSecurityNumber = z.e.f.SocialSecurityNumber,
                   DateOfBirth = z.e.f.DateOfBirth,
                   ClaimantFirstName = z.e.f.ClaimantFirstName,
                   ClaimantMiddleName = z.e.f.ClaimantMiddleName,
                   ClaimantLastName = z.e.f.ClaimantLastName,
                   ClaimantSuffix = z.e.f.ClaimantSuffix,
                   TelephoneNumber = z.e.f.TelephoneNumber,
                   Gender = z.x.Code,
                   GenderCode = z.x.GenderId,
                   Occupation = z.e.f.Occupation,
                   MailingStreetAddress = z.e.f.MailingStreetAddress,
                   MailingCity = z.e.f.MailingCity,
                   MailingStateCode = h.StateCode,
                   MailingState = z.e.f.MailingState,
                   ZipCode = z.e.f.ZipCode,
                   Citizen = z.e.f.Citizen,
                   Ethnicity = z.e.f.Ethnicity,
                   Race = z.e.f.Race,
                   Handicap = z.e.f.Handicap,
                   FederalwithHolding = z.e.f.FederalwithHolding,
                   VeteranStatus = z.e.f.VeteranStatus,
                   Education = z.e.f.Education,
                   AuthorizedAlienNumber = z.e.f.AuthorizedAlienNumber

               }).Where(e => e.Email == EmailID).ToList();

            return result;
        }

        public EmployeeDirectoryDto GetEmployeedirectoryClaimantdetails(string ssn)
        {
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@SSN", string.IsNullOrEmpty(ssn) ? null : ssn)
            };

            var claimantData = this.CallStoredProcedure<ClaimantDto>("dbo.sp_ValidateClaimant", parameters);

            var result = claimantData.FirstOrDefault();

            EmployeeDirectoryDto edDto = new EmployeeDirectoryDto();
            edDto.SocialSecurityNumber = ssn;
            edDto.DateOfBirth = result.DOB;
            edDto.TelephoneNumber = result.PhoneNumber;
            edDto.ClaimantFirstName = result.ClaimantFirstName;
            edDto.ClaimantMiddleName = result.ClaimantMiddleName;
            edDto.ClaimantLastName = result.ClaimantLastName;
            edDto.MailingStreetAddress = result.AddressLine1;
            edDto.MailingCity = result.City;
            edDto.MailingState = this._PartialClaimsContext.States.Where(e => e.StateCode == result.State).Select(x => x.StateId).FirstOrDefault();
            edDto.ZipCode = result.ZIP;
            edDto.Gender = this._PartialClaimsContext.DropdownGenders.Where(e => e.Code == result.Gender).Select(x => x.GenderId).FirstOrDefault();
            edDto.Handicap = this._PartialClaimsContext.DropdownHandicaps.Where(e => e.Code == result.Handicap).Select(x => x.HandicapId).FirstOrDefault();
            edDto.VeteranStatus = this._PartialClaimsContext.DropdownVeterans.Where(e => e.Code == result.Veteran).Select(x => x.VeteranId).FirstOrDefault();
            edDto.Race = this._PartialClaimsContext.DropdownRaces.Where(e => e.Code == result.Race).Select(x => x.RaceId).FirstOrDefault();
            edDto.Ethnicity = this._PartialClaimsContext.DropdownEthnicities.Where(e => e.Code == result.Ethnicity).Select(x => x.EthnicityId).FirstOrDefault();
            edDto.FederalwithHolding = this._PartialClaimsContext.DropdownWithholdings.Where(e => e.Code == result.Withholdings).Select(x => x.WithholdingsId).FirstOrDefault();
            edDto.Citizen = this._PartialClaimsContext.DropdownCitizens.Where(e => e.Code == result.Citizen).Select(x => x.CitizenId).FirstOrDefault();
            edDto.Education = this._PartialClaimsContext.DropdownEducations.Where(e => e.Code == result.Education).Select(x => x.EducationId).FirstOrDefault();


            return edDto;
        }
        public IEnumerable<TResult> CallStoredProcedure<TResult>(string storedProcedureName, params SqlParameter[] parameters)
        {
            var connectionString = _PartialClaimsContext.Database.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }



                    using (var reader = command.ExecuteReader())
                    {
                        var results = new List<TResult>();



                        while (reader.Read())
                        {
                            TResult result = MapDataReaderToTResult<TResult>(reader);
                            results.Add(result);
                        }
                        return results;
                    }
                }
            }
        }
        private TResult MapDataReaderToTResult<TResult>(SqlDataReader reader)
        {
            TResult result = Activator.CreateInstance<TResult>();

            if (reader.HasRows)
            {

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var property = typeof(TResult).GetProperty(reader.GetName(i));
                    if (property != null && reader.IsDBNull(i) == false)
                    {
                        var value = reader.GetValue(i);
                        property.SetValue(result, value);
                    }
                }
            }


            return result;
        }

        public async Task<string> DeleteEmplpyeeDirectoryDetails(string ssn)
        {
            try
            {
                if (this._PartialClaimsContext.EmployeeDirectories.Where(e => e.SocialSecurityNumber == ssn).Any())
                {
                    var existingdata = await this._PartialClaimsContext.EmployeeDirectories.Where(e => e.SocialSecurityNumber == ssn).FirstAsync();
                    this._PartialClaimsContext.EmployeeDirectories.Remove(existingdata);
                    this._PartialClaimsContext.SaveChangesAsync();
                   
                }
                return "Deleted Successfully..";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
     
}
