namespace Domain.Models.Authentication;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; set; }
    public string Password { get; private set; }
    public Role Role { get; set; } = null!;

    public User(Guid id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }

    public User(Guid id, string username, string password, Role role) : this(id, username, password)
    {
        Role = role;
    }
}