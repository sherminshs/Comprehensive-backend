using System;
using System.Collections.Generic;

namespace Comprehensive_backend.Models;

public partial class Retired
{
    public long RetiredId { get; set; }

    public string? RetiredFirstName { get; set; }

    public string? RetiredLastName { get; set; }

    public string RetiredNationalCode { get; set; } = null!;

    public virtual ICollection<Related> Relateds { get; set; } = new List<Related>();
}
