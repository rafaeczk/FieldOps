using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Operators.Contracts;

public interface IOperatorsModuleApi
{
    Task<OperatorId?> GetOperatorIdByAccountId(AccountId accountId);
}
