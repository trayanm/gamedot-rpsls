using GameDot.Core;
using GameDot.Core.Entities;
using GameDot.Core.Enums;
using GameDot.Core.Exceptions;
using GameDot.Core.Helpers;
using GameDot.Core.Services.RandomValueService;

namespace GameDot.Infrastructure
{
    public class GameSessionEngine : IGameSessionEngine
    {
        #region Private Properties
        private readonly GameDotSettings _gameDotSettings;
        private readonly IRandomValueService _randomValueService;
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        public GameSessionEngine(GameDotSettings gameDotSettings, IRandomValueService randomValueService)
        {
            this._gameDotSettings = gameDotSettings;
            this._randomValueService = randomValueService;
        }
        #endregion

        #region Public Methods
        public async Task<ChoiceKind> GetRandomChoice()
        {
            int randomSeed = 0;
            int gameChoicesCount = Gameplay.GameChoices.Count;

            try
            {
                RandomValueResult random = await this._randomValueService.GetRandomValueAsync();
                randomSeed = random.Value;
                randomSeed += gameChoicesCount; // assure that the seed is above cameChoicesCount
            }
            catch (Exception ex)
            {
                if (this._gameDotSettings.UseRandomRedundancy)
                {
                    Random random = new Random();
                    randomSeed = random.Next(1, gameChoicesCount);
                }
                else
                    throw;
            }

            decimal algInx = randomSeed % gameChoicesCount;
            int trueIndex = Convert.ToInt32(algInx);

            ChoiceKind choice = Gameplay.GameChoices[trueIndex];

            return choice;
        }

        public ChoiceKind GetUserChoice(int choiceId)
        {
            ChoiceKind? choice = EnumUtility.ParseOrNull<ChoiceKind>(choiceId);

            if (!choice.HasValue)
            {
                throw new GameDotException("0x2001", "Player choice not found");
            }

            return choice.Value;
        }

        public GameMatchResultKind PlayMatch(ChoiceKind left, ChoiceKind right)
        {
            GameMatchResultKind result = GameMatchResultKind.Lose;

            if (left == right)
            {
                result = GameMatchResultKind.Tie;
            }
            else
            {
                GameRule rule = Gameplay.GameRules.FirstOrDefault(t => t.Choice == left);

                if (rule != null && EnumUtility.IsEnumInRange(right, rule.OverChoices))
                {
                    result = GameMatchResultKind.Win;
                }
            }

            return result;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
