using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Organizations.GetById;

internal sealed class GetOrganizationByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrganizationByIdQuery, OrganizationByIdResponse>
{
    public async Task<Result<OrganizationByIdResponse>> Handle(
        GetOrganizationByIdQuery query,
        CancellationToken cancellationToken)
    {
        OrganizationByIdResponse? organization = await context.Organizations
            .AsNoTracking()
            .Where(o => o.Id == query.OrganizationId)
            .Select(o => new OrganizationByIdResponse
            {
                Id = o.Id,
                Name = o.Name,
                Code = o.Code,
                Country = o.Country,
                Phone = o.Phone,
                FullAddress = o.FullAddress,
                CreationDate = o.CreationDate,
                UpdatingDate = o.UpdatingDate
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (organization is null)
        {
            return Result.Failure<OrganizationByIdResponse>(OrganizationErrors.NotFound(query.OrganizationId));
        }

        return organization;
    }
}
