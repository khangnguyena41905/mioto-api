using MediatR;
using MIOTO.CONTRACT.Abstractions.Shared;

namespace MIOTO.CONTRACT.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>{}