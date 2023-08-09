using System;
using System.Collections.Generic;

namespace PartialZ.DataAccess.PartialZDB;

public partial class State
{
    public int StateId { get; set; }

    public string StateCode { get; set; } = null!;

    public string StateName { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Fipscode { get; set; }

    public string? LowZip { get; set; }

    public string? HighZip { get; set; }
}
