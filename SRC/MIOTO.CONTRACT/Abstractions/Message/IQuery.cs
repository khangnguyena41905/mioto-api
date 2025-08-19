using MediatR;
using MIOTO.CONTRACT.Abstractions.Shared;

namespace MIOTO.CONTRACT.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}