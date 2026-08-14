using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Abstractions.Modules;

internal interface IModule
{
    string Name { get; }
    string Path { get; }
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder builder);
}
