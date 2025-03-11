using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class PassengerDetail
{
    public string PassengerId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? CoachNumber { get; set; }

    public int? SeatNumber { get; set; }

    public bool BookingStatus { get; set; }

    public string TicketId { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
