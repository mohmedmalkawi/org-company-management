using Application.Abstractions.Messaging;

namespace Application.Organizations.GetDetails;

public sealed record GetOrganizationDetailsQuery(Guid OrganizationId) : IQuery<OrganizationDetailsResponse>;
