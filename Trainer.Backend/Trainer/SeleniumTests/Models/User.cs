using Trainer.Enums;

namespace SeleniumTests.Models;

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public UserRole Role { get; set; }
}