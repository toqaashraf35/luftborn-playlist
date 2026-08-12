namespace PlaylistApi.Models;

public class Song
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Artist { get; set; } = string.Empty;
	public int DurationInSeconds {  get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int PlaylistId { get; set; }
    public Playlist? Playlist { get; set; }

}
