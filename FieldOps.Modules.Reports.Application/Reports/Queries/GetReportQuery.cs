using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Application.Reports.Specifications;
using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Reports.Application.Reports.Queries;

public record GetReportQuery(Guid ReportId) : IMessage<ReportDetailsDto?>;

internal sealed class GetReportQueryHandler(IReportsReadRepository repository, ITechnicianModuleApi technicianModuleApi, IContext context) : IMessageHandler<GetReportQuery, ReportDetailsDto?>
{
    public async Task<ReportDetailsDto?> HandleAsync(GetReportQuery message, CancellationToken ct)
    {
        var technicianId = await technicianModuleApi.GetTechnicianIdByAccountId(context.Identity.Id);

        var spec = new GetReportSpecification(
            message.ReportId,
            context.Identity.Role,
            technicianId);

        return await repository.GetAsync(spec);
    }
}
