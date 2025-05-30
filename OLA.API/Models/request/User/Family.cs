namespace OLA.API.Models.request.User;

public class Family : BaseRequest
{
    public string Name { get; set; }
    public string ContactNumber { get; set; }
    public RelationshipType RelationType { get; set; }
    public DateTime DateOfBrith { get; set; }
    public bool IsEmergencyContact { get; set; }
    public Guid? AddressId { get; set; }
    public virtual Address Address { get; set; }
}
