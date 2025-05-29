using Microsoft.AspNetCore.Mvc;
using DutchBlitzBackend.Models;
using System.Collections.Concurrent;
using System;
using System.Linq;

namespace DutchBlitzBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {


        private ConcurrentDictionary<string, GameData> Games = new();


        [HttpPost("create")]
        public IActionResult CreateGame([FromBody] CreateGameRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.GameId))
                return BadRequest("GameId is required");

            if (Games.ContainsKey(request.GameId))
                return Conflict("Game with this ID already exists");

            if (string.IsNullOrWhiteSpace(request.HostPlayerId) || string.IsNullOrWhiteSpace(request.HostPlayerName))
                return BadRequest("Host player ID and name are required");

            var hostPlayer = new Player
            {
                Id = request.HostPlayerId,
                Name = request.HostPlayerName,
                Score = 0,
                IsHost = true,
                IsReady = false
            };

            var game = new GameData
            {
                GameId = request.GameId,
                HostId = hostPlayer.Id,
                Players = new List<Player> { hostPlayer },
                ScoreLimit = request.ScoreLimit,
            };

            Games[request.GameId] = game;
            return Ok(game);
        }

        [HttpPost("leave/{gameId}/{playerId}")]
        public IActionResult LeaveGame(string gameId, string playerId)
        {
            if (!Games.TryGetValue(gameId, out GameData? game)) 
                return NotFound("Game not found");
            var player = game.Players.First(p => p.Id == playerId);
            if (player == null)
                return NotFound("Player not found ");
            game.Players.Remove(player);
            if (game.Players.Count == 0)
                Games.TryRemove(gameId, out _); 
            return Ok(game);
        }

        [HttpPost("delete/{gameid}")]
        public IActionResult DeleteGame(string gameid)
        {
            if (!Games.TryRemove(gameid, out GameData? game)) 
                return NotFound("Game not found");
            return Ok(game);
        }

        [HttpPost("join/{gameId}")]
        public IActionResult JoinGame(string gameId, [FromBody] Player player)
        {
            if (!Games.TryGetValue(gameId, out GameData? game)) 
                return NotFound("Game not found");

            game.Players.Add(player);
            return Ok(game);
        }

        [HttpPost("toggle-ready/{gameId}/{playerId}")]
        public IActionResult ToggleReady(string gameId, string playerId)
        {
            if (!Games.TryGetValue(gameId, out GameData? game)) 
                return NotFound();

            var player = game.Players.FirstOrDefault(p => p.Id == playerId);
            if (player == null)
                return NotFound();

            player.IsReady = !player.IsReady;
            return Ok(player);
        }

        [HttpPost("start/{gameId}")]
        public IActionResult StartGame(string gameId)
        {
            if (!Games.TryGetValue(gameId, out GameData? game))
                return NotFound();

            if (!game.Players.All(p => p.IsReady))
                return BadRequest("Not all players are ready");

            game.GameStatus = true;
            return Ok(game);
        }

        [HttpPost("submit-score/{gameId}")]
        public IActionResult SubmitScore(string gameId, [FromBody] PlayerScore score)
        {
            if (!Games.TryGetValue(gameId, out GameData? game)) 
                return NotFound();

            var round = game.Rounds.LastOrDefault() ?? new Round
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            if (!game.Rounds.Contains(round))
                game.Rounds.Add(round);

            round.Scores.RemoveAll(s => s.PlayerId == score.PlayerId);
            round.Scores.Add(score);

            return Ok(round);
        }

        [HttpGet("{gameId}")]
        public IActionResult GetGame(string gameId)
        {
            if (!Games.TryGetValue(gameId, out GameData? game)) 
                return NotFound();

            return Ok(game);
        }

        [HttpGet()]
        public IActionResult GetAllGames()
        {
            return Ok(Games.Values.ToList());

        }

    }

}
