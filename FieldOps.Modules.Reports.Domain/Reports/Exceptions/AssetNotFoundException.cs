using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions
{
    [Serializable]
    public class AssetNotFoundException(Guid assetId) : BaseException($"Asset with ID '{assetId}' was not found.") { 
    
    }
}
