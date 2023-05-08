using GameDot.Core.Enums;

namespace GameDot.Core
{
    public interface IGameSessionEngine
    {
        Task<ChoiceKind> GetRandomChoice();
        ChoiceKind GetUserChoice(int choiceId);
        GameMatchResultKind PlayMatch(ChoiceKind left, ChoiceKind right);
    }
}
