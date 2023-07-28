using System;
using System.Collections.Generic;

namespace TMS.Api.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    public string? VenueLocation { get; set; }

    public string? VenueType { get; set; }

    public int? VenueCapacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
