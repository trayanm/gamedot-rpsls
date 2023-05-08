using GameDot.Api.Helpers;
using GameDot.Api.Models;
using GameDot.Application.Commands.Play;
using GameDot.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameDot.Api.Controllers
{
    [ApiController]
    [Route("play")]
    public class PlayController : ControllerBase
    {
        #region Private Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public PlayController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Play a round against a bot.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PlayResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] PlayRequestModel model)
        {
            PlayCommand command = new PlayCommand { ChoiceId = model.Player };
            GameMatchResult res = await this._mediator.Send(command);

            PlayResponseModel result = new PlayResponseModel
            {
                Player = (int)res.PlayerChoice,
                Bot = (int)res.BotChoice,
                Results = res.MatchResul.GetStringValue().ToLower()
            };

            return this.Ok(result);
        }
        #endregion
    }
}
