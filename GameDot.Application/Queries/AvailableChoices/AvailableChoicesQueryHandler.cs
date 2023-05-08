using GameDot.Core;
using GameDot.Core.Entities;
using MediatR;

namespace GameDot.Application.Queries.AvailableChoices
{
    public class AvailableChoicesQueryHandler : IRequestHandler<AvailableChoicesQuery, IEnumerable<GameChoice>>
    {
        private readonly GameDotSettings _gameDotSettings;

        public AvailableChoicesQueryHandler(GameDotSettings gameDotSettings)
        {
            this._gameDotSettings = gameDotSettings;
        }

        public async Task<IEnumerable<GameChoice>> Handle(AvailableChoicesQuery request, CancellationToken cancellationToken = default)
        {
            IEnumerable<GameChoice> result = Gameplay.GameChoices.Select(t => new GameChoice { Choice = t });

            return result;
        }
    }
}
