namespace OLA.API.Models.request.User;

public class Address : BaseRequest
{
    public  string Barangay {get; set;}
    public  string AppUserId { get; set; }
    public  string City {get; set;} 
    public  string Purok {get; set;}
}
