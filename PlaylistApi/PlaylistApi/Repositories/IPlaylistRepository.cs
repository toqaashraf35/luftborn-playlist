using PlaylistApi.Models;

namespace PlaylistApi.Repositories
{
    public interface IPlaylistRepository
    {
        Task<Playlist> CreatePlaylist(Playlist playlist);
        Task<bool> AddSongToPlaylist(int playlistId, Song song);
        Task<List<Playlist>> GetPlaylistsByUserId(int userId);
    }
}
