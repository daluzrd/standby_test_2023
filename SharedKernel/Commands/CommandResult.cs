namespace SharedKernel.Commands;

public abstract class CommandResult
{
    protected CommandResult(bool success, string? message = null, object? data = null)
    {
        Messages = new List<string>();
        Success = success;        
        Data = data;

        if (!string.IsNullOrEmpty(message))
        {
            Messages.Add(message);
        }

    }

    protected CommandResult(bool success, List<string> message, object? data = null)
    {
        Messages = new List<string>();
        Success = success;
        Messages.AddRange(message);
        Data = data;
    }
 


    public bool Success { get; set; }
    public object? Data { get; set; }
    public List<string> Messages { get; set; }

    public bool HasErrors()
    {
        return Messages.Any() && !Success;
    }
    public bool HasMessages()
    {
        return Messages.Any() && Success;
    }
    public void AddError(string error)
    {
        Success = false;
        Messages.Add(error);
    }
    public void AddErrors(List<string> errors)
    {
        Success = false;
        Messages.AddRange(errors);
    }
}