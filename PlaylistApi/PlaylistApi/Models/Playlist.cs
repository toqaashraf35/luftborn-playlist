namespace PlaylistApi.Models;

public class Playlist
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int UserId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public List<Song> Songs { get; set; } = new();
}
