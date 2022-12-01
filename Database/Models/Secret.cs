using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class Secret {
  public int Id { get; set; }
  public string? Text { get; set; } = default!;
}