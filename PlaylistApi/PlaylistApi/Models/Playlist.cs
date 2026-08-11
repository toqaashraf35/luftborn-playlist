namespace PlaylistApi.Models;

public class Playlist
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string UserId { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public List<Song> Songs { get; set; } = new();
}
