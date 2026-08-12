using Microsoft.EntityFrameworkCore;
using PlaylistApi.Data;
using PlaylistApi.Models;

namespace PlaylistApi.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _context;

        public PlaylistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Playlist> CreatePlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return playlist;
        }
        
        public async Task<List<Playlist>> GetPlaylistsByUserId(int userId)
        {
            var playlists = await _context.Playlists
                .Where(p => p.UserId == userId)
                .Include(p => p.Songs)
                .ToListAsync();

            return playlists;
        }

        public async Task<Playlist?> UpdatePlaylist(int playlistId, string name)
        {
            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.Id == playlistId);

            if (playlist == null) return null;

            playlist.Name = name;
            playlist.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return playlist;
        }

        public async Task<bool> DeletePlaylist(int playlistId)
        {
            var playlist = await _context.Playlists.FindAsync(playlistId);
            if (playlist == null) return false;

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
