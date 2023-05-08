using GameDot.Core;
using GameDot.Core.Entities;
using GameDot.Core.Enums;
using MediatR;

namespace GameDot.Application.Queries.RandomChoice
{
    public class RandomChoiceQueryHandler : IRequestHandler<RandomChoiceQuery, GameChoice>
    {
        private readonly IGameSessionEngine _gameSessionEngine;

        public RandomChoiceQueryHandler(IGameSessionEngine gameSessionEngine)
        {
            this._gameSessionEngine = gameSessionEngine;
        }

        public async Task<GameChoice> Handle(RandomChoiceQuery request, CancellationToken cancellationToken = default)
        {
            ChoiceKind choice = await this._gameSessionEngine.GetRandomChoice();

            GameChoice result = new GameChoice
            {
                Choice = choice
            };

            return result;
        }
    }
}
