using GameDot.Core.Entities;
using GameDot.Core.Enums;
using GameDot.Core.Helpers;

namespace GameDot.Core
{
    public class Gameplay
    {
        public static List<ChoiceKind> GameChoices;
        public static List<GameRule> GameRules;

        static Gameplay()
        {
            GameChoices = EnumUtility.EnumToList<ChoiceKind>();

            GameRules = new List<GameRule>
            {
                { new GameRule(ChoiceKind.Rock, new List<ChoiceKind> { ChoiceKind.Scissors, ChoiceKind.Lizard }) },
                { new GameRule(ChoiceKind.Paper, new List<ChoiceKind> { ChoiceKind.Rock, ChoiceKind.Spock }) },
                { new GameRule(ChoiceKind.Scissors, new List<ChoiceKind> { ChoiceKind.Paper, ChoiceKind.Lizard }) },
                { new GameRule(ChoiceKind.Lizard, new List<ChoiceKind> { ChoiceKind.Paper, ChoiceKind.Spock }) },
                { new GameRule(ChoiceKind.Spock, new List<ChoiceKind> { ChoiceKind.Rock, ChoiceKind.Scissors }) }
            };
        }
    }
}
