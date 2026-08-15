using Moq;
using PlaylistApi.DTOs;
using PlaylistApi.Models;
using PlaylistApi.Repositories;
using PlaylistApi.Services;
using Xunit;

namespace PlaylistApi.Tests
{
    public class SongServiceTests
    {
        [Fact]
        public async Task AddSongToPlaylist_ShouldReturnSongResponseDto_WhenPlaylistExists()
        {
            var mockRepository = new Mock<ISongRepository>();

            mockRepository
                .Setup(repo => repo.AddSongToPlaylist(It.IsAny<int>(), It.IsAny<Song>()))
                .ReturnsAsync(true);

            var service = new SongService(mockRepository.Object);
            var dto = new SongRequestDto
            {
                Title = "Shape of You",
                Artist = "Ed Sheeran",
                DurationInSeconds = 240
            };
            var result = await service.AddSongToPlaylist(1, dto);

            Assert.NotNull(result);
            Assert.Equal("Shape of You", result.Title);
            Assert.Equal("Ed Sheeran", result.Artist);
        }

        [Fact]
        public async Task AddSongToPlaylist_ShouldReturnNull_WhenPlaylistDoesNotExist()
        {
            var mockRepository = new Mock<ISongRepository>();

            mockRepository
                .Setup(repo => repo.AddSongToPlaylist(It.IsAny<int>(), It.IsAny<Song>()))
                .ReturnsAsync(false);

            var service = new SongService(mockRepository.Object);
            var dto = new SongRequestDto
            {
                Title = "Shape of You",
                Artist = "Ed Sheeran",
                DurationInSeconds = 240
            };
            var result = await service.AddSongToPlaylist(999, dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateSong_ShouldReturnSongResponseDto_WhenSongExists()
        {
            var mockRepository = new Mock<ISongRepository>();

            var fakeUpdatedSong = new Song
            {
                Id = 1,
                Title = "New Title",
                Artist = "Ed Sheeran",
                DurationInSeconds = 250
            };

            mockRepository
                .Setup(repo => repo.UpdateSong(It.IsAny<int>(), It.IsAny<UpdatedSongDto>()))
                .ReturnsAsync(fakeUpdatedSong);

            var service = new SongService(mockRepository.Object);
            var dto = new UpdatedSongDto
            {
                Title = "New Title"
            };

            var result = await service.UpdateSong(1, dto);

            Assert.NotNull(result);
            Assert.Equal("New Title", result.Title);
        }

        [Fact]
        public async Task UpdateSong_ShouldReturnNull_WhenSongDoesNotExist()
        {
            var mockRepository = new Mock<ISongRepository>();

            mockRepository
                .Setup(repo => repo.UpdateSong(It.IsAny<int>(), It.IsAny<UpdatedSongDto>()))
                .ReturnsAsync((Song?)null);

            var service = new SongService(mockRepository.Object);
            var dto = new UpdatedSongDto
            {
                Title = "New Title"
            };
            var result = await service.UpdateSong(999, dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteSong_ShouldReturnTrue_WhenSongExists()
        {
            var mockRepository = new Mock<ISongRepository>();

            mockRepository
                .Setup(repo => repo.DeleteSong(It.IsAny<int>()))
                .ReturnsAsync(true);

            var service = new SongService(mockRepository.Object);
            var result = await service.DeleteSong(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSong_ShouldReturnFalse_WhenSongDoesNotExist()
        {
            var mockRepository = new Mock<ISongRepository>();

            mockRepository
                .Setup(repo => repo.DeleteSong(It.IsAny<int>()))
                .ReturnsAsync(false);

            var service = new SongService(mockRepository.Object);
            var result = await service.DeleteSong(999);

            Assert.False(result);
        }
    }
}