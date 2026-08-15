using Moq;
using PlaylistApi.DTOs;
using PlaylistApi.Models;
using PlaylistApi.Repositories;
using PlaylistApi.Services;
using Xunit;

namespace PlaylistApi.Tests
{
    public class PlaylistServiceTests
    {
        [Fact]
        public async Task CreatePlaylist_ShouldReturnPlaylistResponseDto_WhenDataIsValid()
        {
            var mockRepository = new Mock<IPlaylistRepository>();

            var inputDto = new PlaylistRequestDto
            {
                Name = "Playlist 1",
                UserId = 1
            };
            var fakeCreatedPlaylist = new Playlist
            {
                Id = 1,
                Name = "Playlist 1",
                UserId = 1,
                CreatedAt = DateTime.UtcNow
            };

            mockRepository
                .Setup(repo => repo.CreatePlaylist(It.IsAny<Playlist>()))
                .ReturnsAsync(fakeCreatedPlaylist);

            var service = new PlaylistService(mockRepository.Object);
            var result = await service.CreatePlaylist(inputDto);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Playlist 1", result.Name);
        }

        [Fact]
        public async Task GetPlaylistsByUserId_ShouldReturnPlaylistsWithSongs()
        {
            var mockRepository = new Mock<IPlaylistRepository>();

            var fakePlaylists = new List<Playlist>
            {
                new Playlist
                {
                    Id = 1,
                    Name = "Playlist 1",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow,
                    Songs = new List<Song>
                    {
                        new Song { Id = 1, Title = "Shape of You", Artist = "Ed Sheeran", DurationInSeconds = 240 }
                    }
                }
            };

            mockRepository
                .Setup(repo => repo.GetPlaylistsByUserId(123))
                .ReturnsAsync(fakePlaylists);

            var service = new PlaylistService(mockRepository.Object);
            var result = await service.GetPlaylistsByUserId(123);

            Assert.Single(result);
            Assert.Equal("Playlist 1", result[0].Name);
            Assert.Single(result[0].Songs);
            Assert.Equal("Shape of You", result[0].Songs[0].Title);
        }

        [Fact]
        public async Task DeletePlaylist_ShouldReturnTrue_WhenPlaylistExists()
        {
            var mockRepository = new Mock<IPlaylistRepository>();

            mockRepository
                .Setup(repo => repo.DeletePlaylist(It.IsAny<int>()))
                .ReturnsAsync(true);

            var service = new PlaylistService(mockRepository.Object);
            var result = await service.DeletePlaylist(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeletePlaylist_ShouldReturnFalse_WhenPlaylistDoesNotExist()
        {
            var mockRepository = new Mock<IPlaylistRepository>();

            mockRepository
                .Setup(repo => repo.DeletePlaylist(It.IsAny<int>()))
                .ReturnsAsync(false);

            var service = new PlaylistService(mockRepository.Object);
            var result = await service.DeletePlaylist(999);

            Assert.False(result);
        }
    }

}