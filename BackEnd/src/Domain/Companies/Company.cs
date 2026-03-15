using SharedKernel;

namespace Domain.Companies;

public sealed class Company : Entity
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string FullAddress { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? UpdatingDate { get; set; }
}
