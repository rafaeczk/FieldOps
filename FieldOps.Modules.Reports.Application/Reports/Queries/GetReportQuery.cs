using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Reports.Application.Reports.Queries;

public record GetReportQuery(Guid ReportId) : IMessage<ReportDetailsDto?>;

internal sealed class GetReportQueryHandler(IReportsReadRepository repository) : IMessageHandler<GetReportQuery, ReportDetailsDto?>
{
    public Task<ReportDetailsDto?> HandleAsync(GetReportQuery message, CancellationToken ct)
        => repository.GetAsync(message.ReportId);
}
