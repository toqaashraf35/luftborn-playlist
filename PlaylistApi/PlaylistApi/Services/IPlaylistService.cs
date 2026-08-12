using PlaylistApi.DTOs;

namespace PlaylistApi.Services
{
    public interface IPlaylistService
    {
        Task<PlaylistResponseDto> CreatePlaylist(PlaylistRequestDto dto);
        Task<List<PlaylistResponseDto>> GetPlaylistsByUserId(int userId);
        Task<PlaylistResponseDto?> UpdatePlaylist(int playlistId, string name);
        Task<bool> DeletePlaylist(int playlistId);
    }
}
