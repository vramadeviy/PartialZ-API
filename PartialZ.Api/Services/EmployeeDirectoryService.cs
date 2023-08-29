using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PartialZ.Api.Dtos;
using PartialZ.Api.Services.Interfaces;
using PartialZ.DataAccess.PartialZDB;
using System.Runtime.Intrinsics.X86;

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

                var filerid = await this._PartialClaimsContext.Filers.Where(e => e.Email == empDirectoryDto.Email).Select(x => x.EmployeeId).FirstAsync();
                empDirectoryDto.SocialSecurityNumber = empDirectoryDto.SocialSecurityNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""); /*(206) - (60) - (1868)*/
                empDirectoryDto.TelephoneNumber = empDirectoryDto.TelephoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""); /*(206) - (60) - (1868)*/


                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@FilerId", filerid),
                    new SqlParameter("@SocialSecurityNumber", string.IsNullOrEmpty(empDirectoryDto.SocialSecurityNumber) ? null : empDirectoryDto.SocialSecurityNumber),
                    new SqlParameter("@DateOfBirth", string.IsNullOrEmpty(empDirectoryDto.DateOfBirth.ToString()) ? null : empDirectoryDto.DateOfBirth),
                    new SqlParameter("@TelephoneNumber", string.IsNullOrEmpty(empDirectoryDto.TelephoneNumber) ? null : empDirectoryDto.TelephoneNumber),
                    new SqlParameter("@ClaimantFirstName", string.IsNullOrEmpty(empDirectoryDto.ClaimantFirstName) ? null : empDirectoryDto.ClaimantFirstName),

                    new SqlParameter("@ClaimantMiddleName", string.IsNullOrEmpty(empDirectoryDto.ClaimantMiddleName) ? null : empDirectoryDto.ClaimantMiddleName),
                    new SqlParameter("@ClaimantLastName",  string.IsNullOrEmpty(empDirectoryDto.ClaimantLastName) ? null : empDirectoryDto.ClaimantLastName),
                    new SqlParameter("@ClaimantSuffix",(empDirectoryDto.ClaimantSuffix)==0 ? 0 : empDirectoryDto.ClaimantSuffix),
                    new SqlParameter("@AuthorizedAlienNumber", string.IsNullOrEmpty(empDirectoryDto.AuthorizedAlienNumber) ? null : empDirectoryDto.AuthorizedAlienNumber),
                    new SqlParameter("@MailingStreetAddress", string.IsNullOrEmpty(empDirectoryDto.MailingStreetAddress) ? null : empDirectoryDto.MailingStreetAddress),

                    new SqlParameter("@MailingCity", string.IsNullOrEmpty(empDirectoryDto.MailingCity) ? null : empDirectoryDto.MailingCity),
                    new SqlParameter("@MailingState", (empDirectoryDto.MailingState)==0 ? null : empDirectoryDto.MailingState),
                    new SqlParameter("@ZipCode", string.IsNullOrEmpty(empDirectoryDto.ZipCode) ? null : empDirectoryDto.ZipCode),
                    new SqlParameter("@Gender", (empDirectoryDto.Gender)==0 ? null : empDirectoryDto.Gender),
                    new SqlParameter("@Handicap", (empDirectoryDto.Handicap)==0 ? null : empDirectoryDto.Handicap),

                    new SqlParameter("@VeteranStatus", (empDirectoryDto.VeteranStatus)==0 ? null : empDirectoryDto.VeteranStatus),
                    new SqlParameter("@Race", (empDirectoryDto.Race)==0 ? null : empDirectoryDto.Race),
                    new SqlParameter("@Ethnicity", (empDirectoryDto.Ethnicity)==0 ? null : empDirectoryDto.Ethnicity),
                    new SqlParameter("@Citizen", (empDirectoryDto.Citizen)==0 ? null : empDirectoryDto.Citizen),
                    new SqlParameter("@Education", (empDirectoryDto.Education)==0 ? null : empDirectoryDto.Education),

                    new SqlParameter("@Occupation", string.IsNullOrEmpty(empDirectoryDto.Occupation) ? null : empDirectoryDto.Occupation),
                    new SqlParameter("@FederalwithHolding", (empDirectoryDto.FederalwithHolding)==0 ? null : empDirectoryDto.FederalwithHolding),
                    new SqlParameter("@CreatedDate", DateTime.UtcNow),
                    new SqlParameter("@LastModifedDate", DateTime.UtcNow),
            };

                var claimantData = this.CallStoredProcedure<ClaimantDto>("dbo.InsertUpdateEmployeeDirectories", parameters);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> BulkSaveEmplpyeeDirectoryDetails(List<EmployeeDirectoryListDto> empDirectoryDto)
        {
            try
            {
                int employeeDirectoryID = 0;

                foreach (var item in empDirectoryDto)
                {
                    var filerid = await this._PartialClaimsContext.Filers.Where(e => e.Email == item.Email).Select(x => x.EmployeeId).FirstAsync();
                    var stateId = this._PartialClaimsContext.States.Where(c => c.StateCode == item.MailingState).Select(c => c.StateId).SingleOrDefault();
                    var claimantSuffix = this._PartialClaimsContext.DropdownNameSuffixes.Where(c => c.Code == item.ClaimantSuffix).Select(c => c.NameSuffixId).SingleOrDefault();
                    var gender = this._PartialClaimsContext.DropdownGenders.Where(c => c.Code == item.Gender).Select(c => c.GenderId).SingleOrDefault();
                    var handicap = this._PartialClaimsContext.DropdownHandicaps.Where(c => c.Code == item.Handicap).Select(c => c.HandicapId).SingleOrDefault();
                    var veteranStatus = this._PartialClaimsContext.DropdownVeterans.Where(c => c.Code == item.VeteranStatus).Select(c => c.VeteranId).SingleOrDefault();
                    var race = this._PartialClaimsContext.DropdownRaces.Where(c => c.Code == item.Race).Select(c => c.RaceId).SingleOrDefault();
                    var ethnicity = this._PartialClaimsContext.DropdownEthnicities.Where(c => c.Code == item.Ethnicity).Select(c => c.EthnicityId).SingleOrDefault();
                    var citizen = this._PartialClaimsContext.DropdownCitizens.Where(c => c.Code == item.Citizen).Select(c => c.CitizenId).SingleOrDefault();
                    var education = this._PartialClaimsContext.DropdownEducations.Where(c => c.Code == item.Education).Select(c => c.EducationId).SingleOrDefault();
                    var federalwithdrawing = this._PartialClaimsContext.DropdownWithholdings.Where(c => c.Code == item.FederalwithHolding).Select(c => c.WithholdingsId).SingleOrDefault();

                    SqlParameter[] parameters = new[]
                    {
                    new SqlParameter("@FilerId", filerid),
                    new SqlParameter("@SocialSecurityNumber", string.IsNullOrEmpty(item.SocialSecurityNumber) ? null : item.SocialSecurityNumber),
                    new SqlParameter("@DateOfBirth", string.IsNullOrEmpty(item.DateOfBirth.ToString()) ? null : item.DateOfBirth),
                    new SqlParameter("@TelephoneNumber", string.IsNullOrEmpty(item.TelephoneNumber) ? null : item.TelephoneNumber),
                    new SqlParameter("@ClaimantFirstName", string.IsNullOrEmpty(item.ClaimantFirstName) ? null : item.ClaimantFirstName),

                    new SqlParameter("@ClaimantMiddleName", string.IsNullOrEmpty(item.ClaimantMiddleName) ? null : item.ClaimantMiddleName),
                    new SqlParameter("@ClaimantLastName",  string.IsNullOrEmpty(item.ClaimantLastName) ? null : item.ClaimantLastName),
                    new SqlParameter("@ClaimantSuffix",claimantSuffix==0 ? 0 : claimantSuffix),
                    new SqlParameter("@AuthorizedAlienNumber", string.IsNullOrEmpty(item.AuthorizedAlienNumber) ? null : item.AuthorizedAlienNumber),
                    new SqlParameter("@MailingStreetAddress", string.IsNullOrEmpty(item.MailingStreetAddress) ? null : item.MailingStreetAddress),

                    new SqlParameter("@MailingCity", string.IsNullOrEmpty(item.MailingCity) ? null : item.MailingCity),
                    new SqlParameter("@MailingState",  stateId == 0 ? 0 : stateId),
                    new SqlParameter("@ZipCode", string.IsNullOrEmpty(item.ZipCode) ? null : item.ZipCode),
                    new SqlParameter("@Gender", gender==0 ? null : gender),
                    new SqlParameter("@Handicap", handicap==0 ? null : handicap),

                    new SqlParameter("@VeteranStatus", veteranStatus ==0 ? null : veteranStatus),
                    new SqlParameter("@Race", race==0 ? null : race),
                    new SqlParameter("@Ethnicity", ethnicity==0 ? null : ethnicity),
                    new SqlParameter("@Citizen", citizen==0 ? null : citizen),
                    new SqlParameter("@Education", education==0 ? null : education),

                    new SqlParameter("@Occupation", string.IsNullOrEmpty(item.Occupation) ? null : item.Occupation),
                    new SqlParameter("@FederalwithHolding", federalwithdrawing==0 ? null : federalwithdrawing),
                    new SqlParameter("@CreatedDate", DateTime.UtcNow),
                    new SqlParameter("@LastModifedDate", DateTime.UtcNow),
            };

                    var claimantData = this.CallStoredProcedure<ClaimantDto>("dbo.InsertUpdateEmployeeDirectories", parameters);
                }
                return 1;
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
                   AuthorizedAlienNumber = z.e.f.AuthorizedAlienNumber,
                   Status = z.e.f.Status

               }).Where(x=>x.Email==EmailID && x.Status== "ACTIVE").ToList();

            return result;
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

        public async Task<int> DeleteEmplpyeeDirectoryDetails(string ssn)
        {
            try
            {
                ssn = ssn.Replace("(", "").Replace(")","").Replace("-","").Replace(" ",""); /*(206) - (60) - (1868)*/

                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@SocialSecurityNumber", string.IsNullOrEmpty(ssn) ? null : ssn)
            };

                this.CallStoredProcedure<ClaimantDto>("dbo.DeleteEmployeeDirectories", parameters);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
     
}
