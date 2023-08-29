using System;
using System.Collections.Generic;

namespace PartialZ.DataAccess.PartialZDB;

public partial class EmployeeDirectory
{
    public int EmployeeDirectoryId { get; set; }

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

    public string? Status { get; set; }

    public virtual Filer? Filer { get; set; }
}
