using FieldOps.Modules.Files.Contracts;
using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Application.Reports.Services;
using FieldOps.Modules.Reports.Domain.Outbox;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;
using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Application.Reports.Commands
{
    public record AddReportAssigneeCommand(ReportId ReportId, FileId FileId) : IMessage;

    internal sealed class AddReportAssigneeCommandHandler(IReportsReadRepository repository, IOutboxMessagesRepository outboxRepository, IReportsUnitOfWork unitOfWork,
        IReportEventMapper eventMapper, IFilesModuleApi filesModuleApi) : IMessageHandler<AddReportAssigneeCommand>
    {
        public async Task HandleAsync(AddReportAssigneeCommand message, CancellationToken ct)
        {
            var report = await repository.GetByIdAsync(message.ReportId);

            if (report is null)
                throw new ReportNotFoundException(message.ReportId);

            if (!await filesModuleApi.GetFileExists(message.FileId))
                throw new FileDoesNotExistException(message.FileId);

            report.AddAssignee(message.FileId);
            await outboxRepository.AddAsync([.. eventMapper.Map(report.Events)]);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
