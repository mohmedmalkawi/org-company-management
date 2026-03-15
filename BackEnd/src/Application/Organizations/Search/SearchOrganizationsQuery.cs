using Application.Abstractions.Messaging;

namespace Application.Organizations.Search;

public sealed record SearchOrganizationsQuery(
    string? Name,
    string? Code,
    string? Country,
    int? Page,
    int? PageSize) : IQuery<List<OrganizationSearchResponse>>;
