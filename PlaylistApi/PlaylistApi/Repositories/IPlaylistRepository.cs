using PlaylistApi.Models;

namespace PlaylistApi.Repositories
{
    public interface IPlaylistRepository
    {
        Task<Playlist> CreatePlaylist(Playlist playlist);
        Task<List<Playlist>> GetPlaylistsByUserId(int userId);
        Task<Playlist?> UpdatePlaylist(int playlistId, string name);
        Task<bool> DeletePlaylist(int playlistId);
    }
}
