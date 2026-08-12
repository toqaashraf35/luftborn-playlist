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

        public async Task<bool> AddSongToPlaylist(int playlistId, Song song)
        {
            var playlist = await _context.Playlists.FindAsync(playlistId);
            if (playlist == null)
            {
                return false;
            }
            song.PlaylistId = playlistId;
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Playlist>> GetPlaylistsByUserId(int userId)
        {
            var playlists = await _context.Playlists
                .Where(p => p.UserId == userId)
                .Include(p => p.Songs)
                .ToListAsync();

            return playlists;
        }
    }
}
