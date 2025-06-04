using System;
using System.ComponentModel.DataAnnotations;

public class Feedback
{
    public int Id { get; set; }

    [Required(ErrorMessage = nameof(Name) + " ist erforderlich.")]
    [StringLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = nameof(Message) + " ist erforderlich.")]
    [StringLength(200)]
    public string Message { get; set; }

    [Required(ErrorMessage = nameof(Ratinggrade) + " ist erforderlich.")]
    public byte Ratinggrade { get; set; }

    [Required(ErrorMessage = nameof(Country) + " ist erforderlich.")]
    [StringLength(2)]
    public string Country { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.Now;
}