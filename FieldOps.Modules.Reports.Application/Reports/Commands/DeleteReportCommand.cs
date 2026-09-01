using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Domain.Reports.Repositories;
using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;

namespace FieldOps.Modules.Reports.Application.Reports.Commands;

public record DeleteReportCommand(Guid ReportId) : IMessage;

internal sealed class DeleteReportCommandHandler(IReportsReadRepository readRepository, IReportsWriteRepository writeRepository, IReportsUnitOfWork unitOfWork) : IMessageHandler<DeleteReportCommand>
{
    public async Task HandleAsync(DeleteReportCommand message, CancellationToken ct)
    {
        var report = await readRepository.GetByIdAsync(message.ReportId);
        if (report is null)
            throw new ReportNotFoundException(message.ReportId);

        report.SoftDelete();

        writeRepository.Update(report);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
