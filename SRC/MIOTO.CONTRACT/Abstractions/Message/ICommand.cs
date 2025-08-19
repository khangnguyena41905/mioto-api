using MediatR;
using MIOTO.CONTRACT.Abstractions.Shared;

namespace MIOTO.CONTRACT.Abstractions.Message;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}