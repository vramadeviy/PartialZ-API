using System;
using System.Collections.Generic;

namespace PartialZ.DataAccess.PartialZDB;

public partial class EmployeeWorkHistory
{
    public int Id { get; set; }

    public int EmployerId { get; set; }

    public int EmployeeId { get; set; }

    public string? PayrollEndDay { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModifedDate { get; set; }
}
