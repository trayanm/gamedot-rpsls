using GameDot.Api.Helpers;
using GameDot.Api.Models;
using GameDot.Application.Queries.AvailableChoices;
using GameDot.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameDot.Api.Controllers
{
    [ApiController]
    [Route("choices")]
    public class ChoicesController : ControllerBase
    {
        #region Private Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public ChoicesController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Returns all possible choices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChoiceResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            AvailableChoicesQuery command = new AvailableChoicesQuery();
            IEnumerable<GameChoice> medResponse = await this._mediator.Send(command);

            IEnumerable<ChoiceResponseModel> result = medResponse.Select(t => new ChoiceResponseModel { Id = (int)t.Choice, Name = t.Choice.GetStringValue().ToLower() });

            return this.Ok(result);
        }
        #endregion
    }
}
