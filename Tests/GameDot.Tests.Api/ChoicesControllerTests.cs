using FluentAssertions;
using GameDot.Api.Controllers;
using GameDot.Api.Helpers;
using GameDot.Api.Models;
using GameDot.Application.Queries.AvailableChoices;
using GameDot.Core.Entities;
using GameDot.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameDot.Tests.Api
{
    public class ChoicesControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturn()
        {
            // Arrange
            IEnumerable<GameChoice> mockMedResponse = new List<GameChoice>
            {
                new GameChoice{ Choice = ChoiceKind.Lizard },
                new GameChoice{ Choice = ChoiceKind.Paper }
            };
            IEnumerable<ChoiceResponseModel> expectedResult = mockMedResponse.Select(t => new ChoiceResponseModel
            {
                Id = (int)t.Choice,
                Name = t.Choice.GetStringValue().ToLower()
            });

            Mock<IMediator> mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<AvailableChoicesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockMedResponse);

            ChoicesController controller = new ChoicesController(mediatorMock.Object);

            // Act
            OkObjectResult actualResult = (OkObjectResult)await controller.Get();

            // Assert
            actualResult.Value.Should().BeEquivalentTo(expectedResult);
        }
    }
}