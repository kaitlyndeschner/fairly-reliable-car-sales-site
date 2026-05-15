using System;
using System.Collections.Generic;

namespace FairlyReliableCarSalesSite.Models;

public partial class Enquiry
{
    public int EnquiryId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public int? CarId { get; set; }

    public string Message { get; set; } = null!;

    public DateOnly DateSubmitted { get; set; }

    public virtual Car? Car { get; set; }
}

