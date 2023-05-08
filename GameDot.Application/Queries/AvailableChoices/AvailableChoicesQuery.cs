using GameDot.Core.Entities;
using MediatR;

namespace GameDot.Application.Queries.AvailableChoices
{
    public class AvailableChoicesQuery : IRequest<IEnumerable<GameChoice>>
    {
    }
}
