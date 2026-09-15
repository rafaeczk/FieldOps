using FieldOps.Shared.Abstractions.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.Exceptions
{
    [Serializable]
    public class AssetNotFoundException(Guid assetId) : BaseException($"Asset with ID '{assetId}' was not found.")
    {

    }
}
