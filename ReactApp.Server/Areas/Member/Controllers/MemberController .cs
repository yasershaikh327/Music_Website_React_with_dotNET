using DataAccess.Helper;
using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using ReactApp.Server.ExceptionFilter;



[Area("Member")]
[Route("/[controller]/[action]")]
[MyExceptionFilter]
[ApiController]

public class MemberController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;

    public MemberController(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }


    //Contact Us
    [HttpPost]
    public async Task<JsonResult> Contact([FromBody] Contact contact)
    {
        if (contact == null)
        {
            return new JsonResult(new { message = "Contact data is null" }) { StatusCode = StatusCodes.Status400BadRequest };
        }

        await _memberRepository.ContactData(contact);

        var response = new
        {
            message = "Thank you for Contacting us",
            color = "green"
        };
    

        return new JsonResult(response) { StatusCode = StatusCodes.Status201Created };
    }


    //Register
    [HttpPost]
    public async Task<JsonResult> Registration([FromBody] Registration registration)
    {
        if (registration == null)
        {
            return new JsonResult(new { message = "Registeration data is null" }) { StatusCode = StatusCodes.Status400BadRequest };
        }

        var res = await _memberRepository.RegistrationData(registration);
        if (res != "Exists")
        {
            return new JsonResult("NonExists") ;
        }
        return new JsonResult("Exists") ;
    }


    //Login
    public async Task<JsonResult> Login([FromBody] Login login)
    {
        if (login == null)
        {
            return new JsonResult(new { message = "Login data is null" }) { StatusCode = StatusCodes.Status400BadRequest };
        }

        var user = await _memberRepository.LoginData(login);
        if (user != null)
        {
            var response = new
            {
                message = "Login Successful",
                color = "green",
                Id = user.ToString()
            };
            return new JsonResult(response) { StatusCode = StatusCodes.Status201Created };
        }
        return new JsonResult("Invalid Credentials");
    }



    //Create Playlist
    [HttpPost]
    public async Task<JsonResult> addplaylistt([FromBody] Playlist playlist)
    {
        if (playlist == null)
        {
            return new JsonResult(new { message = "Playlist data is null" }) { StatusCode = StatusCodes.Status400BadRequest };
        }

        await _memberRepository.AddPlaylist(playlist);

        return new JsonResult("Added Successfully") { StatusCode = StatusCodes.Status201Created };
    }


    //Get Playlists
    [HttpGet]
    public async Task<ActionResult<List<ReadPlaylist>>> getplaylistt()
    {
        var playlist = await _memberRepository.GetAllMusicAsync();
        if (playlist == null || playlist.Count == 0)
        {
            return NotFound("No Playlist found.");
        }
        return Ok(playlist);
    }

    //GetPlaylists based on userId
    [HttpGet("{userId}")]
    public async Task<ActionResult<List<ReadPlaylist>>> GetAllMusicAsyncById(string userId)
    {
        var playlist = await _memberRepository.GetAllMusicAsyncById(userId);
        if (playlist == null)
        {
            return NotFound("No Playlist found.");
        }
        return Ok(playlist);
    }


    //Update Playlist
    [HttpPatch("{id}")]
    public async Task<ActionResult<Playlist>> UpdatePlaylist(string id, [FromBody] Playlist updatedMusic)
    {
        var existingPlaylist = await _memberRepository.GetPlaylistByIdAsync(id);
        if (existingPlaylist == null)
        {
            return NotFound();
        }

        // Update fields if they're provided
        if (!string.IsNullOrEmpty(updatedMusic.Description))
            existingPlaylist.Description = updatedMusic.Description;
        if (!string.IsNullOrEmpty(updatedMusic.NameofPlayList))
            existingPlaylist.NameofPlayList = updatedMusic.NameofPlayList;

        await _memberRepository.UpdatePlaylistAsync(id, existingPlaylist);

        return Ok(existingPlaylist);
    }

    //Delete Playlist
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletPlaylist(string id)
    {
        // Retrieve the music item by its ID (you can uncomment and implement this)
        var playlist = await _memberRepository.GetPlaylistByIdAsync(id);
        if (playlist == null)
        {
            return NotFound();
        }

        // Delete the music item
        await _memberRepository.DeletePlaylistAsync(id);

        return new JsonResult(new { message = "Playlist deleted successfully." });
    }

    //Add Music To Playlist
    [HttpPost]
    public async Task<JsonResult> AddMusictoPlaylist([FromBody] MusicToPlaylist playlist)
    {
        // Check if music already exists in the playlist
        var checkIfMusicInPlaylistExists = await _memberRepository.CheckIfMusicInPlaylistExists(playlist);

        if (checkIfMusicInPlaylistExists)
        {
            return new JsonResult(new { message = "Music in Playlist data already exists" })
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        if (playlist == null)
        {
            return new JsonResult(new { message = "Playlist data is null" })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        // Add the music to the playlist
        await _memberRepository.AddMusicPlaylist(playlist);

        return new JsonResult(new { message = "Added Music Successfully" })
        {
            StatusCode = StatusCodes.Status201Created
        };
    }


}

