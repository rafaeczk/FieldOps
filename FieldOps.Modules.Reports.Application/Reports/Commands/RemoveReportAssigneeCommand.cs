using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Application.Reports.Services;
using FieldOps.Modules.Reports.Domain.Outbox;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Reports.Application.Reports.Commands;

public record RemoveReportAssigneeCommand(ReportId ReportId, FileId FileId) : IMessage;

internal sealed class RemoveReportAssigneeCommandHandler(IReportsReadRepository repository, IOutboxMessagesRepository outboxRepository, 
    IReportsUnitOfWork unitOfWork, IReportEventMapper eventMapper) : IMessageHandler<RemoveReportAssigneeCommand>
{
    public async Task HandleAsync(RemoveReportAssigneeCommand message, CancellationToken ct)
    {
        var report = await repository.GetByIdAsync(message.ReportId);

        if (report is null)
            throw new ReportNotFoundException(message.ReportId);

        report.RemoveAttachment(message.FileId);
        await outboxRepository.AddAsync([.. eventMapper.Map(report.Events)]);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
