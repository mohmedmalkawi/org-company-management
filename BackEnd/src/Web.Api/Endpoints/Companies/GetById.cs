using Application.Abstractions.Messaging;
using Application.Companies.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Companies;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("companies/{id:guid}", async (
            Guid id,
            IQueryHandler<GetCompanyByIdQuery, CompanyByIdResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCompanyByIdQuery(id);

            Result<CompanyByIdResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Companies);
    }
}
