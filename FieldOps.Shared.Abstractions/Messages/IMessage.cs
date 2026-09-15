using MediatR;

namespace FieldOps.Shared.Abstractions.Messages;

public interface IMessage<out Result> : IRequest<Result> { }
public interface IMessage : IRequest { }
