using Microsoft.AspNetCore.Mvc;
using PlaylistApi.DTOs;
using PlaylistApi.Services;

namespace PlaylistApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _service;

        public SongController(ISongService service)
        {
            _service = service;
        }

        [HttpPost("{playlistId}")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, SongRequestDto dto)
        {
            var result = await _service.AddSongToPlaylist(playlistId, dto);

            if (result == null)
            {
                return NotFound($"Playlist with id {playlistId} not found.");
            }

            return Ok(result);
        }

        [HttpPatch("{songId}")]
        public async Task<IActionResult> UpdateSong(int songId, UpdatedSongDto dto)
        {
            var result = await _service.UpdateSong(songId, dto);

            if (result == null)
            {
                return NotFound($"Song with id {songId} not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{songId}")]
        public async Task<IActionResult> DeleteSong(int songId)
        {
            var success = await _service.DeleteSong(songId);

            if (!success)
            {
                return NotFound($"Song with id {songId} not found.");
            }

            return Ok($"Song with id {songId} deleted successfully.");
        }
    }
}