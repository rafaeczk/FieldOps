using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Operators.Core.Exceptions
{
    internal class OperatorNotFoundException(Guid id) : BaseException($"Operator with ID {id} not found")
    {
    }
}