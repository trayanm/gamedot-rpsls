using GameDot.Core.Enums;

namespace GameDot.Core.Entities
{
    public class GameMatchResult
    {
        public ChoiceKind PlayerChoice { get; set; }
        public ChoiceKind BotChoice { get; set; }
        public GameMatchResultKind MatchResul { get; set; }
    }
}
