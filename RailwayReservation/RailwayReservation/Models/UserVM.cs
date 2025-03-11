﻿namespace RailwayReservation.Models
{
    public class UserVM
    {
        public string UserId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string Phone { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Role { get; set; }
    }
}
