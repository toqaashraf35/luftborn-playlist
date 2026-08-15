using System.Net;
using System.Net.Http.Json;
using PlaylistApi.DTOs;
using Xunit;

namespace PlaylistApi.Tests.Integration
{
    public class PlaylistsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PlaylistsControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreatePlaylist_ShouldReturnOk_WithValidData()
        {
            var newPlaylist = new PlaylistRequestDto
            {
                Name = "Playlist 1",
                UserId = 1
            };

            var response = await _client.PostAsJsonAsync("/api/Playlists", newPlaylist);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<PlaylistResponseDto>();
            Assert.NotNull(result);
            Assert.Equal("Playlist 1", result.Name);
        }

        [Fact]
        public async Task CreateThenGetPlaylist_ShouldReturnCreatedPlaylist()
        {
            var newPlaylist = new PlaylistRequestDto
            {
                Name = "Road Trip",
                UserId = 456
            };
            var createResponse = await _client.PostAsJsonAsync("/api/Playlists", newPlaylist);
            createResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync("/api/Playlists/456");
            getResponse.EnsureSuccessStatusCode();
            var playlists = await getResponse.Content.ReadFromJsonAsync<List<PlaylistResponseDto>>();

            Assert.NotNull(playlists);
            Assert.Contains(playlists, p => p.Name == "Road Trip");
        }

        [Fact]
        public async Task DeletePlaylist_ShouldReturnNotFound_WhenPlaylistDoesNotExist()
        {
            var response = await _client.DeleteAsync("/api/Playlists/99999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddSongToPlaylist_ShouldReturnOk_WhenPlaylistExists()
        {
            var newPlaylist = new PlaylistRequestDto { Name = "Chill Vibes", UserId = 789 };
            var createResponse = await _client.PostAsJsonAsync("/api/Playlists", newPlaylist);
            var createdPlaylist = await createResponse.Content.ReadFromJsonAsync<PlaylistResponseDto>();
            var newSong = new SongRequestDto
            {
                Title = "Sunflower",
                Artist = "Post Malone",
                DurationInSeconds = 158
            };

            var response = await _client.PostAsJsonAsync($"/api/Song/{createdPlaylist!.Id}", newSong);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<SongResponseDto>();
            Assert.Equal("Sunflower", result!.Title);
        }
    }
}