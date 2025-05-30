namespace OLA.Business.Models.response;

public class CreateUserResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}
