namespace FieldOps.Modules.Operators.Contracts;

public interface IOperatorsModuleApi
{
    Task<Guid?> GetOperatorIdByAccountId(Guid accountId);
}
