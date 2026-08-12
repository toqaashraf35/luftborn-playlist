using System.Xml.Linq;
using PlaylistApi.DTOs;
using PlaylistApi.Models;
using PlaylistApi.Repositories;

namespace PlaylistApi.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _repository;

        public SongService(ISongRepository repository)
        {
            _repository = repository;
        }

        public async Task<SongResponseDto?> AddSongToPlaylist(int playlistId, SongRequestDto dto)
        {
            var song = new Song
            {
                Title = dto.Title,
                Artist = dto.Artist,
                DurationInSeconds = dto.DurationInSeconds
            };

            var success = await _repository.AddSongToPlaylist(playlistId, song);
            if (!success) return null;
 
            return new SongResponseDto
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                DurationInSeconds = song.DurationInSeconds,
                AddedAt = song.AddedAt,
                UpdatedAt = song.UpdatedAt, 
            };
        }

        public async Task<SongResponseDto?> UpdateSong(int songId, UpdatedSongDto dto)
        {
            var updatedSong = await _repository.UpdateSong(songId, dto);
            if (updatedSong == null) return null;

            return new SongResponseDto
            {
                Id = updatedSong.Id,
                Title = updatedSong.Title,
                Artist = updatedSong.Artist,
                DurationInSeconds = updatedSong.DurationInSeconds,
                AddedAt = updatedSong.AddedAt,
                UpdatedAt = updatedSong.UpdatedAt,
            };
        }

        public async Task<bool> DeleteSong(int songId)
        {
            return await _repository.DeleteSong(songId);
        }


    }
}
