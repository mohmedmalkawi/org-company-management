namespace Application.Organizations.GetById;

public sealed class OrganizationByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string FullAddress { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? UpdatingDate { get; set; }
}
