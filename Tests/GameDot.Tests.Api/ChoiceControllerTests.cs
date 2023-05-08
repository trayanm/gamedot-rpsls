using FluentAssertions;
using GameDot.Api.Controllers;
using GameDot.Api.Helpers;
using GameDot.Api.Models;
using GameDot.Application.Queries.RandomChoice;
using GameDot.Core.Entities;
using GameDot.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameDot.Tests.Api
{
    public class ChoiceControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturn()
        {
            // Arrange
            GameChoice mockMedResponse = new GameChoice
            {
                Choice = ChoiceKind.Lizard
            };
            ChoiceResponseModel expectedResult = new ChoiceResponseModel
            {
                Id = (int)mockMedResponse.Choice,
                Name = mockMedResponse.Choice.GetStringValue().ToLower()
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<RandomChoiceQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockMedResponse);

            ChoiceController controller = new ChoiceController(mediatorMock.Object);

            // Act
            OkObjectResult actualResult = (OkObjectResult)await controller.Get();

            // Assert
            actualResult.Value.Should().BeEquivalentTo(expectedResult);
        }
    }
}