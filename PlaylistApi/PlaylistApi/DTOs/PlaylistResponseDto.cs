namespace PlaylistApi.DTOs
{
    public class PlaylistResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SongResponseDto> Songs { get; set; } = new();
    }
}
