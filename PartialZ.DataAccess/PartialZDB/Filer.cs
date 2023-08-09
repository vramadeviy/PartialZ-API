using System;
using System.Collections.Generic;

namespace PartialZ.DataAccess.PartialZDB;

public partial class Filer
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? BusinessTitle { get; set; }

    public string? PhoneNumber { get; set; }

    public int? IsVerified { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModifedDate { get; set; }

    public int? LoginOtp { get; set; }

    public int? IsPartialUnitApproved { get; set; }

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual ICollection<EmployeeDirectory> EmployeeDirectories { get; set; } = new List<EmployeeDirectory>();
}
