namespace OLA.API.Models.request.User;

public class Address : BaseRequest
{
    public required string Barangay {get; set;}
    public required string City {get; set;} 
    public required string Purok {get; set;}
}
