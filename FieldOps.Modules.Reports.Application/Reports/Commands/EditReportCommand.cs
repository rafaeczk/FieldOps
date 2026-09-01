using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Domain.Reports.Repositories;
using FieldOps.Modules.Reports.Domain.Reports.ValueObjects;
using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;

namespace FieldOps.Modules.Reports.Application.Reports.Commands;

public record EditReportCommand(Guid ReportId, string Note, Address Address) : IMessage;

internal sealed class EditReportCommandHandler(IReportsReadRepository readRepository, IReportsWriteRepository writeRepository, IReportsUnitOfWork unitOfWork) : IMessageHandler<EditReportCommand>
{
    public async Task HandleAsync(EditReportCommand message, CancellationToken ct)
    {
        var report = await readRepository.GetByIdAsync(message.ReportId);

        if (report is null)
            throw new ReportNotFoundException(message.ReportId);

        report.ChangeNote(message.Note);
        report.ChangeAddress(message.Address);

        writeRepository.Update(report);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
