namespace Mvc.Models;

public class GenericResponseViewModel
{
    public Guid Id { get; private set; }
    public IList<string> Messages { get; private set; }
    public bool Success { get; private set; }

    public GenericResponseViewModel(Guid id, IList<string> messages, bool success)
    {
        Id = id;
        Messages = messages;
        Success = success;
    }
}
