using MediatR;
using MIOTO.CONTRACT.Abstractions.Shared;

namespace MIOTO.CONTRACT.Abstractions.Message;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result> 
    where TCommand : ICommand {}

public interface ICommandHandler<TCommand,TResponse> : IRequestHandler<TCommand, Result<TResponse>> 
    where TCommand : ICommand<TResponse>{}