using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class User {
  [Key]
  public string Username { get; set; } = default!;
  public string Password { get; set; } = default!;
}