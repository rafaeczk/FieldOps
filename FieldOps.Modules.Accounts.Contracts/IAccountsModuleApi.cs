namespace FieldOps.Modules.Accounts.Contracts;

public interface IAccountsModuleApi
{
    Task<bool> CheckAccountEmailIsTaken(string email);
}
