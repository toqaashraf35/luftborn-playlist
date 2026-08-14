namespace PlaylistApi.DTOs
{
    public class UpdatedSongDto
    {
        public string? Title { get; set; } 
        public string? Artist { get; set; } 
        public int? DurationInSeconds { get; set; }
    }
}
