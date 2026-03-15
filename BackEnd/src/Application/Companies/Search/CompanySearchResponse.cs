namespace Application.Companies.Search;

public sealed class CompanySearchResponse
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string OrganizationName { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string Phone { get; set; }
}
