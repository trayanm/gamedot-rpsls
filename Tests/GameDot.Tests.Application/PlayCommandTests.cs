using FluentAssertions;
using GameDot.Application.Commands.Play;
using GameDot.Core;
using GameDot.Core.Entities;
using GameDot.Core.Enums;
using Moq;
using Xunit;

namespace GameDot.Tests.Application
{
    public class PlayCommandTests
    {
        [Fact]
        public async Task PlayCommand_ShouldReturn()
        {
            // Arrange
            PlayCommand command = new PlayCommand { ChoiceId = (int)ChoiceKind.Lizard };
            GameMatchResult expectedResult = new GameMatchResult
            {
                PlayerChoice = ChoiceKind.Lizard,
                BotChoice = ChoiceKind.Lizard,
                MatchResul = GameMatchResultKind.Tie
            };

            Mock<IGameSessionEngine> mockGameSessionEngine = new Mock<IGameSessionEngine>();
            mockGameSessionEngine.Setup(x => x.GetRandomChoice()).ReturnsAsync(ChoiceKind.Lizard);
            mockGameSessionEngine.Setup(x => x.GetUserChoice(It.IsAny<int>())).Returns(ChoiceKind.Lizard);
            mockGameSessionEngine.Setup(x => x.PlayMatch(It.IsAny<ChoiceKind>(), It.IsAny<ChoiceKind>())).Returns(GameMatchResultKind.Tie);

            PlayCommandHandler handler = new PlayCommandHandler(mockGameSessionEngine.Object);

            // Act
            GameMatchResult actualResult = await handler.Handle(command, default);

            // Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}