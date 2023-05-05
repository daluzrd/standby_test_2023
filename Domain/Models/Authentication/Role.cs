namespace Domain.Models.Authentication;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}