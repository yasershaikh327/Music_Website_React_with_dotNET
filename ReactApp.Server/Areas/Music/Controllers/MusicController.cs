using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NuGet.Protocol.Core.Types;
using ReactApp.Server.ExceptionFilter;
using System.Linq;

namespace ReactApp.Server.Areas.Music.Controllers
{
    [Area("Music")]
    [Route("/[controller]/[action]")]
    [MyExceptionFilter]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicRepository _musicRepository;
        public string albumPhotoPath {  get; set; }
        public string audioPath {  get; set; }
        public MusicController(IMusicRepository musicRepository) 
        {
            _musicRepository = musicRepository; 
        }

        //Fetch List of Music
        [HttpGet]
        public async Task<ActionResult<List<Musicc>>> GetMusic()
        {
            var musicList = await _musicRepository.GetAllMusicAsync();
            if (musicList == null || musicList.Count == 0)
            {
                return NotFound("No music found.");
            }

            return Ok(musicList);
        }

        //   fetch(`https://localhost:7094/Music/getMusicByBasedonPlaylist/${iddd}`)
        //Fetch List of Music BAsed on Playlist ID
        [HttpGet("{musicDataId}")]
        public async Task<ActionResult<List<MusicToPlaylist>>> GetMusicByBasedOnPlaylist(string musicDataId)
        {
            var musicList = await _musicRepository.GetMusicByMusicDataId(musicDataId);
            var musicListHugeDataList = await _musicRepository.GetAllMusicAsync();

            // Check if musicList is null or empty
            if (musicList == null || musicList.Count == 0)
            {
                return NotFound("No music found.");
            }

            // Extract musicListId from musicList
            var musicListIds = musicList.Select(m => m.musicListId).ToList();

            // Filter musicListHugeDataList to get matching entries
            var matchingMusic = musicListHugeDataList
                .Where(m => musicListIds.Contains(m.Id)).ToList();

            // Check if matchingMusic is empty
            if (matchingMusic.Count == 0)
            {
                return NotFound("No matching music found in the huge data list.");
            }

            return Ok(matchingMusic); // Return 200 OK with the list of matching music
        }




        //Add Music
        [HttpPost]
        public async Task<JsonResult> AddMusic([FromForm] Musicc music, IFormFile audioFile, IFormFile albumPhoto)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(ModelState);
            }

            if (music == null)
            {
                return new JsonResult("Music data is null.");
            }

            // Store data in MongoDB
            // await _musicRepository.;

            // Store the audio file
            if (audioFile != null && audioFile.Length > 0)
            {
                audioPath = Path.Combine("..", "reactapp.client" ,"Album", "Music", audioFile.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(audioPath)); // Ensure directory exists

                using (var stream = new FileStream(audioPath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(stream);
                }
            }
            else
            {
                return new JsonResult("Audio file is not provided.");
            }

            // Store the album photo
            if (albumPhoto != null && albumPhoto.Length > 0)
            {
               // string albumPhotoPath = Path.Combine("..", "DataAccess", "Audio", "AlbumPhoto", albumPhoto.FileName);
                albumPhotoPath = Path.Combine("..", "reactapp.client", "Album", "AlbumPhoto", albumPhoto.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(albumPhotoPath)); // Ensure directory exists

                using (var stream = new FileStream(albumPhotoPath, FileMode.Create))
                {
                    await albumPhoto.CopyToAsync(stream);
                }
            }
            else
            {
                return new JsonResult("Album photo is not provided.");
            }

            //Store ALL Data
            music.albumPhotoUrl = "Album\\AlbumPhoto\\" + albumPhoto.FileName;
            music.AudioUrl = "Album\\Music\\" + audioFile.FileName;
            await _musicRepository.AddMusicDb(music);

            return new JsonResult(new { message = "Music added successfully!" });
        }



        //Update Music
        [HttpPatch("{id}")]
        public async Task<ActionResult<DisplayMusicDto>> UpdateMusic(string id, [FromBody] DisplayMusicDto updatedMusic)
        {
            var existingMusic = await _musicRepository.GetMusicByIdAsync(id);
            if (existingMusic == null)
            {
                return NotFound();
            }

            // Update fields if they're provided
            if (!string.IsNullOrEmpty(updatedMusic.Title))
                existingMusic.Title = updatedMusic.Title;
            if (!string.IsNullOrEmpty(updatedMusic.Artist))
                existingMusic.Artist = updatedMusic.Artist;
            if (!string.IsNullOrEmpty(updatedMusic.Genre))
                existingMusic.Genre = updatedMusic.Genre;
            if (updatedMusic.Year > 0)
                existingMusic.Year = updatedMusic.Year;

            await _musicRepository.UpdateAsync(id, existingMusic);

            return Ok(existingMusic);
        }
    




        //Delete Music
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(string id)
        {
            // Retrieve the music item by its ID (you can uncomment and implement this)
            var music = await _musicRepository.GetMusicByIdAsync(id);
            if (music == null)
            {
                return NotFound();
            }

            // Delete the music item
            await _musicRepository.DeleteMusicAsync(id);

            return new JsonResult(new { message = "Music deleted successfully." });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusicListCount(string id)
        {
            var count = await _musicRepository.GetMusicListCountAsync(id);
            return Ok(count);
        }


    }

}
