using PlaylistApi.DTOs;

namespace PlaylistApi.Services
{
    public interface ISongService
    {
        Task<SongResponseDto?> AddSongToPlaylist(int playlistId, SongRequestDto dto);
        Task<SongResponseDto?> UpdateSong(int songId, UpdatedSongDto dto);
        Task<bool> DeleteSong(int songId);
    }
}
