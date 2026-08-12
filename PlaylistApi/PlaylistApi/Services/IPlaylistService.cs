using PlaylistApi.DTOs;

namespace PlaylistApi.Services
{
    public interface IPlaylistService
    {
        Task<PlaylistResponseDto> CreatePlaylist(PlaylistRequestDto dto);
        Task<bool> AddSongToPlaylist(int playlistId, SongRequestDto dto);
        Task<List<PlaylistResponseDto>> GetPlaylistsByUserId(int userId);
    }
}
