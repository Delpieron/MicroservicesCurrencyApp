using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiMicroService.Models;

public class Currency
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public required string Symbol { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,8)")]
    public decimal Price { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public User? User { get; set; }
}