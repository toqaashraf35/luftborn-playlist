using PlaylistApi.DTOs;
using PlaylistApi.Models;
using PlaylistApi.Repositories;

namespace PlaylistApi.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _repository;

        public PlaylistService(IPlaylistRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlaylistResponseDto> CreatePlaylist(PlaylistRequestDto dto)
        {
            var playlist = new Playlist
            {
                Name = dto.Name,
                UserId = dto.UserId
            };
            var created = await _repository.CreatePlaylist(playlist);
            return new PlaylistResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                CreatedAt = created.CreatedAt,
                Songs = new List<SongResponseDto>()
            };
        }

        public async Task<bool> AddSongToPlaylist(int playlistId, SongRequestDto dto)
        {
            var song = new Song
            {
                Title = dto.Title,
                Artist = dto.Artist,
                DurationInSeconds = dto.DurationInSeconds
            };
            return await _repository.AddSongToPlaylist(playlistId, song);
        }

        public async Task<List<PlaylistResponseDto>> GetPlaylistsByUserId(int userId)
        {
            var playlists = await _repository.GetPlaylistsByUserId(userId);
            return playlists.Select(p => new PlaylistResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                CreatedAt = p.CreatedAt,
                Songs = p.Songs.Select(s => new SongResponseDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    DurationInSeconds = s.DurationInSeconds
                }).ToList()
            }).ToList();
        }
    }
}
