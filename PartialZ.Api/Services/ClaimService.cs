using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

                var filerid = await this._PartialClaimsContext.Filers.Where(e => e.Email == claimDto.Email).Select(x => x.EmployeeId).FirstAsync();
                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@FilerId", filerid),
                    new SqlParameter("@SocialSecurityNumber", string.IsNullOrEmpty(claimDto.SocialSecurityNumber) ? null : claimDto.SocialSecurityNumber),
                    new SqlParameter("@DateOfBirth", string.IsNullOrEmpty(claimDto.DateOfBirth.ToString()) ? null : claimDto.DateOfBirth),
                    new SqlParameter("@TelephoneNumber", string.IsNullOrEmpty(claimDto.TelephoneNumber) ? null : claimDto.TelephoneNumber),
                    new SqlParameter("@ClaimantFirstName", string.IsNullOrEmpty(claimDto.ClaimantFirstName) ? null : claimDto.ClaimantFirstName),

                    new SqlParameter("@ClaimantMiddleName", string.IsNullOrEmpty(claimDto.ClaimantMiddleName) ? null : claimDto.ClaimantMiddleName),
                    new SqlParameter("@ClaimantLastName",  string.IsNullOrEmpty(claimDto.ClaimantLastName) ? null : claimDto.ClaimantLastName),
                    new SqlParameter("@ClaimantSuffix",(claimDto.ClaimantSuffix)==0 ? 0 : claimDto.ClaimantSuffix),
                    new SqlParameter("@AuthorizedAlienNumber", string.IsNullOrEmpty(claimDto.AuthorizedAlienNumber) ? null : claimDto.AuthorizedAlienNumber),
                    new SqlParameter("@MailingStreetAddress", string.IsNullOrEmpty(claimDto.MailingStreetAddress) ? null : claimDto.MailingStreetAddress),

                    new SqlParameter("@MailingCity", string.IsNullOrEmpty(claimDto.MailingCity) ? null : claimDto.MailingCity),
                    new SqlParameter("@MailingState", (claimDto.MailingState)==0 ? null : claimDto.MailingState),
                    new SqlParameter("@ZipCode", string.IsNullOrEmpty(claimDto.ZipCode) ? null : claimDto.ZipCode),
                    new SqlParameter("@Gender", (claimDto.Gender)==0 ? null : claimDto.Gender),
                    new SqlParameter("@Handicap", (claimDto.Handicap)==0 ? null : claimDto.Handicap),

                    new SqlParameter("@VeteranStatus", (claimDto.VeteranStatus)==0 ? null : claimDto.VeteranStatus),
                    new SqlParameter("@Race", (claimDto.Race)==0 ? null : claimDto.Race),
                    new SqlParameter("@Ethnicity", (claimDto.Ethnicity)==0 ? null : claimDto.Ethnicity),
                    new SqlParameter("@Citizen", (claimDto.Citizen)==0 ? null : claimDto.Citizen),
                    new SqlParameter("@Education", (claimDto.Education)==0 ? null : claimDto.Education),

                    new SqlParameter("@Occupation", string.IsNullOrEmpty(claimDto.Occupation) ? null : claimDto.Occupation),
                    new SqlParameter("@FederalwithHolding", (claimDto.FederalwithHolding)==0 ? null : claimDto.FederalwithHolding),
                    new SqlParameter("@WeekEndingDate", (claimDto.WeekEndingDate)!= null ? null : claimDto.WeekEndingDate),
                    new SqlParameter("@LastDateWorked", string.IsNullOrEmpty(claimDto.LastDateWorked) ? null : claimDto.LastDateWorked),

                    new SqlParameter("@Earnings", (claimDto.Earnings)==0 ? null : claimDto.Earnings),
                    new SqlParameter("@VacationPay", (claimDto.VacationPay)==0 ? null : claimDto.VacationPay),
                    new SqlParameter("@HolidayPay", (claimDto.HolidayPay)==0 ? null : claimDto.HolidayPay),
                    new SqlParameter("@OtherPay", (claimDto.OtherPay)==0 ? null : claimDto.OtherPay),
                    new SqlParameter("@OtherStateWages", (claimDto.OtherStateWages)==0 ? null : claimDto.OtherStateWages),

                    new SqlParameter("@CreatedDate", DateTime.UtcNow),
                    new SqlParameter("@LastModifedDate", DateTime.UtcNow),
            };

                var claimantData = this.CallStoredProcedure<ClaimantDto>("dbo.InsertUpdateClaims", parameters);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClaimantDto GetClaimClaimantdetails(string ssn)
        {
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@SSN", string.IsNullOrEmpty(ssn) ? null : ssn)
            };

            //var claimantData = this.CallStoredProcedure<ClaimantDto>("dbo.sp_ValidateClaimant", parameters);

            //var result = claimantData.FirstOrDefault();

            //ClaimantDto edDto = new ClaimantDto();
            //edDto.SocialSecurityNumber = ssn;
            //edDto.DateOfBirth = result.DOB;
            //edDto.TelephoneNumber = result.PhoneNumber;
            //edDto.ClaimantFirstName = result.ClaimantFirstName;
            //edDto.ClaimantMiddleName = result.ClaimantMiddleName;
            //edDto.ClaimantLastName = result.ClaimantLastName;
            //edDto.MailingStreetAddress = result.AddressLine1;
            //edDto.MailingCity = result.City;
            //edDto.MailingState = this._PartialClaimsContext.States.Where(e => e.StateCode == result.State).Select(x => x.StateId).FirstOrDefault();
            //edDto.ZipCode = result.ZIP;
            //edDto.Gender = this._PartialClaimsContext.DropdownGenders.Where(e => e.Code == result.Gender).Select(x => x.GenderId).FirstOrDefault();
            //edDto.Handicap = this._PartialClaimsContext.DropdownHandicaps.Where(e => e.Code == result.Handicap).Select(x => x.HandicapId).FirstOrDefault();
            //edDto.VeteranStatus = this._PartialClaimsContext.DropdownVeterans.Where(e => e.Code == result.Veteran).Select(x => x.VeteranId).FirstOrDefault();
            //edDto.Race = this._PartialClaimsContext.DropdownRaces.Where(e => e.Code == result.Race).Select(x => x.RaceId).FirstOrDefault();
            //edDto.Ethnicity = this._PartialClaimsContext.DropdownEthnicities.Where(e => e.Code == result.Ethnicity).Select(x => x.EthnicityId).FirstOrDefault();
            //edDto.FederalwithHolding = this._PartialClaimsContext.DropdownWithholdings.Where(e => e.Code == result.Withholdings).Select(x => x.WithholdingsId).FirstOrDefault();
            //edDto.Citizen = this._PartialClaimsContext.DropdownCitizens.Where(e => e.Code == result.Citizen).Select(x => x.CitizenId).FirstOrDefault();
            //edDto.Education = this._PartialClaimsContext.DropdownEducations.Where(e => e.Code == result.Education).Select(x => x.EducationId).FirstOrDefault();


            return null;
        }

        public List<ClaimantDto> GetClaimDetails(string EmailID)
        {
            //var result = this._PartialClaimsContext.Claims
            //  .Join(this._PartialClaimsContext.Filers,
            //  f => f.FilerId, s => s.EmployeeId,
            //  (f, s) => new { f, s })
            //    .Join(this._PartialClaimsContext.DropdownGenders, e => e.f.Gender, x => x.GenderId,
            //    (e, x) => new { e, x })
            //    .Join(this._PartialClaimsContext.States, e => e.e.f.MailingState, x => x.StateId,
            //   (z, h) => new ClaimantDto
            //   {
            //Email = z.e.s.Email,
            //SocialSecurityNumber = z.e.f.SocialSecurityNumber,
            //DateOfBirth = z.e.f.DateOfBirth,
            //ClaimantFirstName = z.e.f.ClaimantFirstName,
            //ClaimantMiddleName = z.e.f.ClaimantMiddleName,
            //ClaimantLastName = z.e.f.ClaimantLastName,
            //ClaimantSuffix = z.e.f.ClaimantSuffix,
            //TelephoneNumber = z.e.f.TelephoneNumber,
            //Gender = z.x.Code,
            //GenderCode = z.x.GenderId,
            //Occupation = z.e.f.Occupation,
            //MailingStreetAddress = z.e.f.MailingStreetAddress,
            //MailingCity = z.e.f.MailingCity,
            //MailingStateCode = h.StateCode,
            //MailingState = z.e.f.MailingState,
            //ZipCode = z.e.f.ZipCode,
            //Citizen = z.e.f.Citizen,
            //Ethnicity = z.e.f.Ethnicity,
            //Race = z.e.f.Race,
            //Handicap = z.e.f.Handicap,
            //FederalwithHolding = z.e.f.FederalwithHolding,
            //VeteranStatus = z.e.f.VeteranStatus,
            //Education = z.e.f.Education,
            //AuthorizedAlienNumber = z.e.f.AuthorizedAlienNumber

            // }).Where(e => e.Email == EmailID).ToList();

            // return result;
            return null;
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
    }
}
