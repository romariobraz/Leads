using System.ComponentModel.DataAnnotations;

namespace LeadQualifier.Api.DTOs;

public class ChatMessageDto
{
    [Required]
    public string Message { get; set; }
}
