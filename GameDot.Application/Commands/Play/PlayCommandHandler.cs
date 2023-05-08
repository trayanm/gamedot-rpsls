using GameDot.Core;
using GameDot.Core.Entities;
using GameDot.Core.Enums;
using MediatR;

namespace GameDot.Application.Commands.Play
{
    public class PlayCommandHandler : IRequestHandler<PlayCommand, GameMatchResult>
    {
        private readonly IGameSessionEngine _gameSessionEngine;

        public PlayCommandHandler(IGameSessionEngine gameSessionEngine)
        {
            this._gameSessionEngine = gameSessionEngine;
        }

        public async Task<GameMatchResult> Handle(PlayCommand request, CancellationToken cancellationToken)
        {
            ChoiceKind userChoice = this._gameSessionEngine.GetUserChoice(request.ChoiceId);
            ChoiceKind botChoice = await this._gameSessionEngine.GetRandomChoice();

            GameMatchResultKind matchResult = this._gameSessionEngine.PlayMatch(userChoice, botChoice);

            GameMatchResult result = new GameMatchResult
            {
                PlayerChoice = userChoice,
                BotChoice = botChoice,
                MatchResul = matchResult
            };

            return result;
        }
    }
}
