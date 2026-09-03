using System;
namespace FieldOps.Modules.Assets.Core.DTOs
{
    public record AssetDto(Guid Id, string Name, string Manufacturer, string SerialNumber);
}
