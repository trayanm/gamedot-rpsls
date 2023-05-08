using GameDot.Core.Entities;
using MediatR;

namespace GameDot.Application.Commands.Play
{
    public class PlayCommand : IRequest<GameMatchResult>
    {
        public int ChoiceId { get; set; }
    }
}
