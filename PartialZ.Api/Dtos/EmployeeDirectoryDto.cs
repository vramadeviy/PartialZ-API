namespace PartialZ.Api.Dtos
{
    public class EmployeeDirectoryDto
    {
        public int EmployeeDirectoryId { get; set; }
        public string Email { get; set; }
        public int? FilerId { get; set; }

        public string? SocialSecurityNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? ClaimantFirstName { get; set; }

        public string? ClaimantMiddleName { get; set; }

        public string? ClaimantLastName { get; set; }

        public int? ClaimantSuffix { get; set; }

        public string? AuthorizedAlienNumber { get; set; }

        public string? MailingStreetAddress { get; set; }

        public string? MailingCity { get; set; }

        public int? MailingState { get; set; }

        public string? ZipCode { get; set; }

        public int? Gender { get; set; }

        public int? Handicap { get; set; }

        public int? VeteranStatus { get; set; }

        public int? Race { get; set; }

        public int? Ethnicity { get; set; }

        public int? FederalwithHolding { get; set; }

        public int? Citizen { get; set; }

        public int? Education { get; set; }

        public string? Occupation { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifedDate { get; set; }
    }

    public class EmployeeDirectoryListDto
    {
        public int EmployeeDirectoryId { get; set; }
        public string Email { get; set; }
        public int? FilerId { get; set; }

        public string? SocialSecurityNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? ClaimantFirstName { get; set; }

        public string? ClaimantMiddleName { get; set; }

        public string? ClaimantLastName { get; set; }

        public string? ClaimantSuffix { get; set; }

        public string? AuthorizedAlienNumber { get; set; }

        public string? MailingStreetAddress { get; set; }

        public string? MailingCity { get; set; }

        public string? MailingState { get; set; }

        public string? ZipCode { get; set; }

        public string? Gender { get; set; }

        public string? Handicap { get; set; }

        public string? VeteranStatus { get; set; }

        public string? Race { get; set; }

        public string? Ethnicity { get; set; }

        public string? FederalwithHolding { get; set; }

        public string? Citizen { get; set; }

        public string? Education { get; set; }

        public string? Occupation { get; set; }
        public string? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifedDate { get; set; }
    }

    public class EmployeeDirectoryDetailsDto
    {
        public string Email { get; set; }

        public string SocialSecurityNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }
        public string Gender { get; set; }
        public int? GenderCode { get; set; }

        public string Occupation { get; set; }

        public string MailingAddress { get; set; }

        public string? ClaimantFirstName { get; set; }

        public string? ClaimantMiddleName { get; set; }
        public string? ClaimantLastName { get; set; }

        public int? ClaimantSuffix { get; set; }

        public string? AuthorizedAlienNumber { get; set; }
        public string? MailingStreetAddress { get; set; }

        public string? MailingCity { get; set; }

        public int? MailingState { get; set; }
        public string? MailingStateCode { get; set; }

        public string? ZipCode { get; set; }

        public int? Handicap { get; set; }

        public int? VeteranStatus { get; set; }

        public int? Race { get; set; }

        public int? Ethnicity { get; set; }

        public int? FederalwithHolding { get; set; }

        public int? Citizen { get; set; }

        public int? Education { get; set; }

        public string? Status { get; set; }


    }
    public class ClaimantDto
    {
        public string PhoneNumber { get; set; }

        public string? ClaimantFirstName { get; set; }

        public string? ClaimantMiddleName { get; set; }

        public string? ClaimantLastName { get; set; } 

        public string? AuthorizedAlienNumber { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }
        public string? ZIP { get; set; }
        public string? Gender { get; set; }

        public string? Handicap { get; set; }
        public string? Veteran { get; set; }

        public string? Race { get; set; }
        public string? Ethnicity { get; set; }
        public string? Withholdings { get; set; }
        public string? Citizen { get; set; }
        public string? Education { get; set; }

        public DateTime? DOB { get; set; }
    }
    public class StateDto
    { 
        public int StateId { get; set; }
        public string StateCode { get; set; } = null!;
    }
    public class DropdownsDto
    {
        public List<RaceDto> raceDto  { get; set; } = null!;
        public List<VeteranDto> veteranDto { get; set; } = null!;
        public List<WithholdingDto> withholdingDto { get; set; } = null!;
        public List<OtherWageDto> otherWageDto { get; set; } = null!;
        public List<NameSuffixDto> nameSuffixDto { get; set; } = null!;


        public List<HandicapDto> handicapDto { get; set; } = null!;
        public List<GenderDto> genderDto { get; set; } = null!;
        public List<EthnicityDto> ethnicityDto { get; set; } = null!;
        public List<CitizenDto> citizenDto { get; set; } = null!;
        public List<EducationDto> educationDto { get; set; } = null!;

    }
    public class RaceDto
    {
        public int RaceId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class VeteranDto
    {
        public int VeteranId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class WithholdingDto
    {
        public int WithholdingsId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class OtherWageDto
    {
        public int OtherWagesId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class NameSuffixDto
    {
        public int NameSuffixId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class HandicapDto
    {
        public int HandicapId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class GenderDto
    {
        public int GenderId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class EthnicityDto
    {
        public int EthnicityId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class EducationDto
    {
        public int EducationId { get; set; }

        public string Code { get; set; } = null!;
    }
    public class CitizenDto
    {
        public int CitizenId { get; set; }

        public string Code { get; set; } = null!;
    }

}
