using FluentAssertions;
using GameDot.Api.Controllers;
using GameDot.Api.Helpers;
using GameDot.Api.Models;
using GameDot.Application.Commands.Play;
using GameDot.Core.Entities;
using GameDot.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameDot.Tests.Api
{
    public class PlayControllerTests
    {
        [Fact]
        public async Task Post_ShouldReturn()
        {
            // Arrange
            PlayRequestModel testModel = new PlayRequestModel { Player = (int)ChoiceKind.Lizard };

            GameMatchResult mockMedResponse = new GameMatchResult
            {
                PlayerChoice = ChoiceKind.Lizard,
                BotChoice = ChoiceKind.Lizard,
                MatchResul = GameMatchResultKind.Tie
            };

            PlayResponseModel expectedResult = new PlayResponseModel
            {
                Player = (int)ChoiceKind.Lizard,
                Bot = (int)ChoiceKind.Lizard,
                Results = GameMatchResultKind.Tie.GetStringValue().ToLower()
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<PlayCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockMedResponse);

            PlayController controller = new PlayController(mediatorMock.Object);

            // Act
            OkObjectResult actualResult = (OkObjectResult)await controller.Post(testModel);

            // Assert
            actualResult.Value.Should().BeEquivalentTo(expectedResult);
        }
    }
}