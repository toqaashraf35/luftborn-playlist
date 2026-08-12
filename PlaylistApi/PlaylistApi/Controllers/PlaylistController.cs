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
        
        [HttpPost("{playlistId}/songs")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, SongRequestDto dto)
        {
            var result = await _service.AddSongToPlaylist(playlistId, dto);
            if (!result)
            {
                return NotFound($"Playlist with Id {playlistId} not found.");
            }
            return Ok("Song added successfully!");
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPlaylistsByUserId(int userId)
        {
            var result = await _service.GetPlaylistsByUserId(userId);
            return Ok(result);
        }
    }
}