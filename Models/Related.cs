using System;
using System.Collections.Generic;

namespace Comprehensive_backend.Models;

public partial class Related
{
    public long RelatedId { get; set; }

    public string? RelatedFirstName { get; set; }

    public string? RelatedLastName { get; set; }

    public long RetiredId { get; set; }

    public virtual Retired Retired { get; set; } = null!;
}
