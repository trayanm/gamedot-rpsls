using GameDot.Api.Helpers;
using GameDot.Api.Models;
using GameDot.Application.Queries.RandomChoice;
using GameDot.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameDot.Api.Controllers
{
    [ApiController]
    [Route("choice")]
    public class ChoiceController : ControllerBase
    {
        #region Private Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public ChoiceController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Returns a randomly generated choice.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ChoiceResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            RandomChoiceQuery command = new RandomChoiceQuery();
            GameChoice medResponse = await this._mediator.Send(command);

            ChoiceResponseModel result = new ChoiceResponseModel { Id = (int)medResponse.Choice, Name = medResponse.Choice.GetStringValue().ToLower() };

            return this.Ok(result);
        }
        #endregion
    }
}
