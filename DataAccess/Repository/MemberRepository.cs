using DataAccess.DbContext;
using DataAccess.Mapper;
using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using DataAccess.Service;
using DataAccess.ServiceModel;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using static DataAccess.Models.Ui.MusicToPlaylist;

public interface IMemberRepository
{
    Task ContactData(Contact cm);
    Task<string> RegistrationData(Registration cm);
    Task<ObjectId?> LoginData(Login cm);
    Task AddPlaylist(Playlist playlist);
    Task<List<ReadPlaylist>> GetAllMusicAsync();
    Task<Playlist> GetPlaylistByIdAsync(string id);
    Task UpdatePlaylistAsync(string id, Playlist model);
    Task DeletePlaylistAsync(string id);
    Task<string> AddMusicPlaylist(MusicToPlaylist playlist);
    Task<bool> CheckIfMusicInPlaylistExists(MusicToPlaylist playlist);
    Task<List<ReadPlaylist>> GetAllMusicAsyncById(string id);
}

public class MemberRepository : IMemberRepository
{
    private readonly IMongoCollection<Contact> _contactCollection;
    private readonly IMongoCollection<Registration> _registerCollection;
    private readonly IMongoCollection<Playlist> _playlistCollection;
    private readonly IMongoCollection<MusicToPlaylist> _musictoPlaylistCollection;
    private readonly IDtoMemberMapper _contactDtoMapper;
    private readonly IMemberMappper _contactMapper;
    private readonly IRegistrationDtoMapper _registrationDtoMapper;
    private readonly IRegistrationMapper _registrationMapper;
    private readonly IPlaylistDToMapper _playlistDToMapper;
    private readonly IPlaylistMapper _playlistMapper;
    private readonly ILoginDtoMapper _loginDtoMapper;
    private readonly ILoginMapper _loginMapper;
    private readonly IDtoMusictoPlaylistMapper _dtomusictoplaylistmapper;
    private readonly IMusictoPlaylistMapper _musictoplaylistmapper;
    private readonly IEmailService _emailService;
    private readonly AdminCredits _adminCredits;

    public MemberRepository(IDtoMusictoPlaylistMapper dtomusictoplaylistmapper, IMusictoPlaylistMapper musictoplaylistmapper, IPlaylistDToMapper playlistDToMapper, IPlaylistMapper playlistMapper, IMongoClient client, IEmailService emailService, IDtoMemberMapper contactDtoMapper, IMemberMappper contactMapper, IRegistrationDtoMapper registrationDtoMapper, IRegistrationMapper registrationMapper, ILoginDtoMapper loginDtoMapper, ILoginMapper loginMapper, AdminCredits adminCredits)
    {
        var database = client.GetDatabase("Product");
        _contactCollection = database.GetCollection<Contact>("Contact");
        _registerCollection = database.GetCollection<Registration>("Register");
        _playlistCollection = database.GetCollection<Playlist>("Playlist");
        _musictoPlaylistCollection = database.GetCollection<MusicToPlaylist>("MusictoPlaylist");
        _emailService = emailService;
        _contactDtoMapper = contactDtoMapper;
        _contactMapper = contactMapper;
        _registrationDtoMapper = registrationDtoMapper;
        _registrationMapper = registrationMapper;
        _loginDtoMapper = loginDtoMapper;
        _loginMapper = loginMapper;
        _adminCredits = adminCredits;
        _playlistDToMapper = playlistDToMapper;
        _playlistMapper = playlistMapper;
        _dtomusictoplaylistmapper = dtomusictoplaylistmapper;
        _musictoplaylistmapper = musictoplaylistmapper;
    }

    //Add playList
    public async Task AddPlaylist(Playlist playlist)
    {
        var mapDTo = _playlistDToMapper.Map(playlist);
        var map = _playlistMapper.Map(mapDTo);
        await _playlistCollection.InsertOneAsync(map);
    }



    // Create Contact
    public async Task ContactData(Contact cm)
    {
        // Insert the product into the collection
        var emailServiceModel = new EmailServiceModel();
        emailServiceModel.Email = cm.Email;
        emailServiceModel.Body = "<h1 style='color:pink'>Hello WOrld</h1>";
        _emailService.SendEmail(emailServiceModel);
        var mapDTo = _contactDtoMapper.Map(cm);
        var map = _contactMapper.Map(mapDTo);
        await _contactCollection.InsertOneAsync(map);
    }



    //Get List of Playlists
    public async Task<List<ReadPlaylist>> GetAllMusicAsync()
    {
        // Fetch the list of music documents from MongoDB
        var playLists = await _playlistCollection.Find(new BsonDocument()).ToListAsync();

        // Convert ObjectId to string in the mapping process
        var data = playLists.Select(m => new ReadPlaylist
        {
            Id = m.Id.ToString(), // Convert ObjectId to string
            Description = m.Description,
            NameofPlayList = m.NameofPlayList,
        }).ToList();

        return data;
    }



    //Get List of Playlists
    public async Task<List<ReadPlaylist>> GetAllMusicAsyncById(string id)
    {
        // Fetch all music documents from MongoDB for the given user ID
        var musicList = await _playlistCollection.Find(m => m.userid == id).ToListAsync();

        // If no music is found, return an empty list
        if (musicList == null || musicList.Count == 0)
        {
            return new List<ReadPlaylist>();
        }

        // Map the music documents to ReadPlaylist objects
        var data = musicList.Select(music => new ReadPlaylist
        {
            Id = music.Id.ToString(), // Convert ObjectId to string
            Description = music.Description,
            NameofPlayList = music.NameofPlayList,
        }).ToList();

        return data;
    }




    //Read Playlist
    public async Task<Playlist> GetPlaylistByIdAsync(string id)
    {
        return await _playlistCollection.Find(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync();
    }

    //Delete Playlist By Id
    public async Task DeletePlaylistAsync(string id)
    {
        // Delete from music to playlist collection
        var musicToPlaylistResult = await _musictoPlaylistCollection.DeleteOneAsync(x => x.musicDataId == id);

        // Delete from playlist collection
        var playlistResult = await _playlistCollection.DeleteOneAsync(x => x.Id == new ObjectId(id));

        // Optionally, you can check if the deletions were successful
        if (musicToPlaylistResult.DeletedCount == 0 && playlistResult.DeletedCount == 0)
        {
            throw new Exception("No playlists found to delete.");
        }
    }


    //Fetch Login Data
    public async Task<ObjectId?> LoginData(Login cm)
    {
        if (_adminCredits.username == cm.Email)
        {
            return _adminCredits.role;
        }

        var dto = _loginDtoMapper.Map(cm);
        var map = _loginMapper.Map(dto);

        // Find a user by email and password
        var user = await _registerCollection
            .Find(user => user.Email == map.Email && user.Password == map.Password) // Ensure password is hashed if applicable
            .FirstOrDefaultAsync();

        return user?.Id; // Return ObjectId if user is found, else null
    }



    // Create Registration
    public async Task<string> RegistrationData(Registration cm)
    {
        //Check if User Already Exists Email
        var user = await _registerCollection
           .Find(user => user.Email == cm.Email && user.Password == cm.Password) // Ensure password is hashed if applicable
           .FirstOrDefaultAsync();

        if (user == null)
        {
            // Insert the product into the collection
            var mapDTo = _registrationDtoMapper.Map(cm);
            var map = _registrationMapper.Map(mapDTo);
            await _registerCollection.InsertOneAsync(map);
            return "Register SuccessFul";
        }
        return "Exists";
    }

    //Update Music By Id
    public async Task UpdatePlaylistAsync(string id, Playlist model)
    {
        await _playlistCollection.ReplaceOneAsync(x => x.Id == new ObjectId(id), model);
    }

    //Add Music To Playlist
    public async Task<string> AddMusicPlaylist(MusicToPlaylist playlist)
    {
        // Insert the product into the collection
        var mapDTo = _dtomusictoplaylistmapper.Map(playlist);
        var map = _musictoplaylistmapper.Map(mapDTo);
        await _musictoPlaylistCollection.InsertOneAsync(map);
        return "Music Added To Playlist SuccessFully";
    }

    // Check if Music In Playlist Exists
    public async Task<bool> CheckIfMusicInPlaylistExists(MusicToPlaylist playlist)
    {
        var existingMusic = await _musictoPlaylistCollection
            .Find(x => x.musicDataId == playlist.musicDataId && x.musicListId == playlist.musicListId)
            .FirstOrDefaultAsync();

        return existingMusic != null;
    }




}

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value);
        _database = client.GetDatabase(configuration.GetSection("MongoDbSettings:Product").Value);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
