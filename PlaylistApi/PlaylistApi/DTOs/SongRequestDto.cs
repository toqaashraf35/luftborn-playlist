namespace PlaylistApi.DTOs
{
    public class SongRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public int DurationInSeconds { get; set; }
    }
}
