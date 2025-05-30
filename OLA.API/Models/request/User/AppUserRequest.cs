namespace OLA.API.Models.request.User;

public class AppUserRequest
{
    public string Id { get; set; }
    public bool Active { get; set; }
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string? MiddleName {get; set;}

    public string? UserName {get; set;}

    public string Email {get; set;}

    public string Password { get; set;}
    public string PhoneNumber {get; set;}

    public ICollection<Address> Addresses { get; set; } = new List<Address>();

    public ICollection<Family> Families { get; set; } = new List<Family>();
}
