using DutchBlitzBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DutchBlitzBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DutchBlitzController : ControllerBase
    {
        private readonly ILogger<DutchBlitzController> _logger;
        const string errorMessage = "An error has occuered.";

        public DutchBlitzController(ILogger<DutchBlitzController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/addPlayer")]
        public IActionResult AddPlayer(Player player)
        {
            if (player == null)
                return BadRequest("A player must be provided");

            try
            {
                // addPlayer

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("api/editPlayer")]
        public IActionResult EditPlayer(string id, string newName)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(errorMessage);

            if (string.IsNullOrEmpty(newName))
                return BadRequest("Please enter a name");

            try
            {
                // editPlayer

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/removePlayer")]
        public IActionResult RemovePlayer(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest(errorMessage);

            try
            {
                // removePlayer

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/setRoundScore")]
        public IActionResult SetRoundScore(Player player) {
            if (player == null)
                return BadRequest("A player must be provided");

            try
            {
                // setRoundScore

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/startNextRound")]
        public IActionResult StartNextRound(Player[] players) {
            if (players == null)
                return BadRequest(errorMessage);

            try
            {
                // startNextRound

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/deleteRound")]
        public IActionResult DeleteRound(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest(errorMessage);

            try
            {
                // deleteRound

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("api/resetPlayerScores")]
        public IActionResult ResetPlayerScores(Player[] players) {
            if (players == null)
                return BadRequest(errorMessage);

            try
            {
                // resetPlayerScores

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/loadPlayers")]
        public IActionResult LoadPlayers(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest(errorMessage);

            try
            {
                // loadPlayers

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/loadRounds")]
        public IActionResult LoadRounds(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(errorMessage);

            try
            {
                // loadRounds

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/loadGame")]
        public IActionResult LoadGame(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(errorMessage);

            try
            {
                // loadGame

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
