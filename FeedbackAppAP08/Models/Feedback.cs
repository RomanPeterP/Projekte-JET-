using FeedbackAppAP08.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Feedback
{
    public int FeedbackId { get; set; }

    [Required(ErrorMessage = nameof(Name) + " ist erforderlich.")]
    [StringLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = nameof(Message) + " ist erforderlich.")]
    [StringLength(200)]
    [DisplayName("Nachricht")]
    public string Message { get; set; }

    [Required(ErrorMessage = nameof(Ratinggrade) + " ist erforderlich.")]
    [DisplayName("Schulnote")]
    public byte Ratinggrade { get; set; }

    [Required(ErrorMessage = nameof(Country) + " ist erforderlich.")]
    [StringLength(2)]
    [DisplayName("Land")]
    public string Country { get; set; }

    [DisplayName("Abgegeben am")]
    public DateTime SubmittedAt { get; set; } = DateTime.Now;

    [NotMapped] // Weil wir kein ViewModel haben (nicht empfohlen)
    [JsonIgnore]
    public IEnumerable<IFormFile>? DocsFromWeb { get; set; }

    public virtual ICollection<Doc> Documents { get; set; } = new List<Doc>();
}