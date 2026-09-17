using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class TooManyReportAssetsException() : BaseException("Report has a maximum of 10 assets.")
{
}
