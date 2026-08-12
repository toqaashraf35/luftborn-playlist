using Microsoft.AspNetCore.Mvc;
using PlaylistApi.DTOs;
using PlaylistApi.Services;

namespace PlaylistApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _service;

        public PlaylistsController(IPlaylistService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist(PlaylistRequestDto dto)
        {
            var result = await _service.CreatePlaylist(dto);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPlaylistsByUserId(int userId)
        {
            var result = await _service.GetPlaylistsByUserId(userId);
            return Ok(result);
        }

        [HttpPatch("{playlistId}")]
        public async Task<IActionResult> UpdatePlaylist(int playlistId, [FromBody] string name)
        {
            var result = await _service.UpdatePlaylist(playlistId, name);

            if (result == null)
            {
                return NotFound($"Playlist with id {playlistId} not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{playlistId}")]
        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            var success = await _service.DeletePlaylist(playlistId);

            if (!success)
            {
                return NotFound($"Playlist with id {playlistId} not found.");
            }

            return Ok($"Playlist with id {playlistId} deleted successfully.");
        }
    }
}