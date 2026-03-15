using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Organizations.GetDetails;

internal sealed class GetOrganizationDetailsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrganizationDetailsQuery, OrganizationDetailsResponse>
{
    public async Task<Result<OrganizationDetailsResponse>> Handle(
        GetOrganizationDetailsQuery query,
        CancellationToken cancellationToken)
    {
        OrganizationDetailsResponse? organization = await context.Organizations
            .AsNoTracking()
            .Where(o => o.Id == query.OrganizationId)
            .Select(o => new OrganizationDetailsResponse
            {
                Phone = o.Phone,
                Country = o.Country,
                FullAddress = o.FullAddress
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (organization is null)
        {
            return Result.Failure<OrganizationDetailsResponse>(OrganizationErrors.NotFound(query.OrganizationId));
        }

        return organization;
    }
}
