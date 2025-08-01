﻿namespace TRSAP09V2.Models;

public class ContactInfo
{
    public int ContactInfoId { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
