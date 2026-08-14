# Luftborn Playlist API

A RESTful API for creating and managing music playlists, built with ASP.NET Core 8.

## Business Requirements

1. User can create a playlist
2. User can add songs to their playlist
3. User can fetch all their playlists
4. **Bonus:** Delete/Update endpoints for both playlists and songs

## Tech Stack

- **ASP.NET Core 8** — Web API framework
- **PostgreSQL** — relational database
- **EF Core 8** — ORM for data access
- **Docker Compose** — containerized database setup
- **Swagger** — API testing and documentation

### Why PostgreSQL?

The data is relational — a playlist has many songs — so data integrity mattered, and a relational database enforces that naturally through foreign keys and cascade rules. EF Core also has strong, well-supported integration with PostgreSQL (via Npgsql), making the two a good fit together.

## Architecture

The project follows a **layered architecture**:

| Layer | Responsibility |
|---|---|
| **Controllers** | Handle HTTP requests and responses |
| **Services** | Contain business logic; convert between models and DTOs |
| **Repositories** | Talk to the database directly through EF Core |
| **Models** | Core database entities (`Playlist`, `Song`) |

> **Note:** Since there is no authentication system in place, no `User` entity was created. `userId` is currently passed as a plain value rather than resolved from an authenticated identity.

## Project Structure

```
??? Controllers/
?   ??? PlaylistsController.cs
?   ??? SongController.cs
??? Models/
?   ??? Playlist.cs
?   ??? Song.cs
??? DTOs/
?   ??? PlaylistRequestDto.cs
?   ??? PlaylistResponseDto.cs
?   ??? SongRequestDto.cs
?   ??? SongResponseDto.cs
?   ??? UpdatedSongDto.cs
??? Repositories/
?   ??? IPlaylistRepository.cs
?   ??? PlaylistRepository.cs
?   ??? ISongRepository.cs
?   ??? SongRepository.cs
??? Services/
?   ??? IPlaylistService.cs
?   ??? PlaylistService.cs
?   ??? ISongService.cs
?   ??? SongService.cs
??? Data/
?   ??? AppDbContext.cs
??? Migrations/
    ??? ...
```

## Endpoints

### Playlists — base route `/api/Playlists`

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/Playlists` | Create a playlist |
| `GET` | `/api/Playlists/{userId}` | Get all playlists for a user |
| `PATCH` | `/api/Playlists/{playlistId}` | Update playlist name |
| `DELETE` | `/api/Playlists/{playlistId}` | Delete a playlist (songs are deleted automatically via cascade delete) |

### Songs — base route `/api/Song`

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/Song/{playlistId}` | Add a song to a playlist |
| `PATCH` | `/api/Song/{songId}` | Update a song — supports partial update (a single field can be sent) |
| `DELETE` | `/api/Song/{songId}` | Delete a song |

## How to Run

## Running with Docker

### Prerequisites

- Docker Desktop

### Run the application

Clone the repository:

```bash
git clone https://github.com/toqaashraf35/luftborn-playlist.git
cd playlistAPI
```

### Run:

docker compose up --build

### The API will be available at:

http://localhost:8081/swagger

## Additional Info

- **Repository:** https://github.com/toqaashraf35/luftborn-playlist.git
- **Folder name:** playlistAPI
- _[add any other notes, e.g. full dockerization, tests]_