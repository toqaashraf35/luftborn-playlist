using Microsoft.EntityFrameworkCore;
using PlaylistApi.Models;
using PlaylistApi.Data;
using PlaylistApi.DTOs;

namespace PlaylistApi.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext _context;

        public SongRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSongToPlaylist(int playlistId, Song song)
        {
            var playlist = await _context.Playlists.FindAsync(playlistId);
            if (playlist == null) return false;
            
            song.PlaylistId = playlistId;

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Song?> UpdateSong(int songId, UpdatedSongDto updatedSong)
        {
            var song = await _context.Songs.FindAsync(songId);
            if (song == null) return null;

            if (updatedSong.Title != null)
            {
                song.Title = updatedSong.Title;
            }

            if (updatedSong.Artist != null)
            {
                song.Artist = updatedSong.Artist;
            }

            if (updatedSong.DurationInSeconds.HasValue)
            {
                song.DurationInSeconds = updatedSong.DurationInSeconds.Value;
            }
            song.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return song;
        }
        
        public async Task<bool> DeleteSong(int songId)
        {
            var song = await _context.Songs.FindAsync(songId);
            if (song == null) return false;

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return true;
        }   
    }
}
