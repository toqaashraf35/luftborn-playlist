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
                UpdatedAt = created.UpdatedAt,
                Songs = new List<SongResponseDto>()
            };
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
                    DurationInSeconds = s.DurationInSeconds,
                    AddedAt = s.AddedAt,
                    UpdatedAt = s.UpdatedAt
                }).ToList()
            }).ToList();
        }

        public async Task<PlaylistResponseDto?> UpdatePlaylist(int playlistId, string name)
        {
            var updatedPlaylist = await _repository.UpdatePlaylist(playlistId, name);
            if (updatedPlaylist == null) return null;

            return new PlaylistResponseDto
            {
                Id = updatedPlaylist.Id,
                Name = updatedPlaylist.Name,
                CreatedAt = updatedPlaylist.CreatedAt,
                UpdatedAt = updatedPlaylist.UpdatedAt,
                Songs = updatedPlaylist.Songs.Select(s => new SongResponseDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    DurationInSeconds = s.DurationInSeconds,
                    AddedAt = s.AddedAt,
                    UpdatedAt = s.UpdatedAt,
                }).ToList()
            };
        }

        public async Task<bool> DeletePlaylist(int playlistId)
        {
            return await _repository.DeletePlaylist(playlistId);
        }
    }
}
