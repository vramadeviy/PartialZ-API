using System;
using System.Collections.Generic;

namespace PartialZ.DataAccess.PartialZDB;

public partial class DropdownZip
{
    public int Zipid { get; set; }

    public string Zip { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;
}
