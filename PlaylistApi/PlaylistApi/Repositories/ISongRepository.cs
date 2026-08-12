using PlaylistApi.DTOs;
using PlaylistApi.Models;

namespace PlaylistApi.Repositories
{
    public interface ISongRepository
    {
        Task<bool> AddSongToPlaylist(int playlistId, Song song);
        Task<Song?> UpdateSong(int songId, UpdatedSongDto updatedSong);
        Task<bool> DeleteSong(int songId);
    }
}
