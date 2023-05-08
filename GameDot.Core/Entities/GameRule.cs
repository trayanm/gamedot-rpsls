using GameDot.Core.Enums;

namespace GameDot.Core.Entities
{
    public class GameRule
    {
        public ChoiceKind Choice { get; set; }
        public List<ChoiceKind> OverChoices { get; set; }

        public GameRule()
        {

        }

        public GameRule(ChoiceKind choice, List<ChoiceKind> overChoices)
        {
            this.Choice = choice;
            this.OverChoices = overChoices;
        }
    }
}
