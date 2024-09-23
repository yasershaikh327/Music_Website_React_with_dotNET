using DataAccess.Mapper;
using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using DataAccess.ServiceModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMusicRepository
    {
        Task<List<MusiccDTO>> GetAllMusicAsync();
        Task AddMusicDb(Musicc cm);
        Task<Musicc> GetMusicByIdAsync(string id);
        Task<List<MusicToPlaylist>> GetMusicByMusicDataId(string musicDataId);
        Task DeleteMusicAsync(string id);
        Task UpdateAsync(string id, Musicc model);
        Task<int> GetMusicListCountAsync(string musicDataId);
    }
    public class MusicRepository : IMusicRepository
    {
        private readonly IMongoCollection<Musicc> _musicCollection;
        private readonly IMongoCollection<MusicToPlaylist> _musictoPlaylistCollection;
        private readonly IDtoMusicMapper _dtomapper;
        private readonly IMusicMapper _mapper;

        public MusicRepository(IMongoClient client, IDtoMusicMapper dtomapper, IMusicMapper mapper) 
        {
            _dtomapper = dtomapper;
            _mapper = mapper;
            var database = client.GetDatabase("Product");
            _musicCollection = database.GetCollection<Musicc>("Musicc");
            _musictoPlaylistCollection = database.GetCollection<MusicToPlaylist>("MusictoPlaylist");
        }


        // Read
        public async Task<List<MusiccDTO>> GetAllMusicAsync()
        {
            // Fetch the list of music documents from MongoDB
            var musicList = await _musicCollection.Find(new BsonDocument()).ToListAsync();

            // Convert ObjectId to string in the mapping process
            var data = musicList.Select(m => new MusiccDTO
            {
                Id = m.Id.ToString(), // Convert ObjectId to string
                Title = m.Title,
                Artist = m.Artist,
                Genre = m.Genre,
                Year = m.Year,
                AlbumPhotoUrl = m.albumPhotoUrl,
                AudioUrl = m.AudioUrl
            }).ToList();

            return data;
        }

        


        //Store
        public async Task AddMusicDb(Musicc cm)
        {
            var mapDTo = _dtomapper.Map(cm);
            var map = _mapper.Map(mapDTo);
            await _musicCollection.InsertOneAsync(map);
        }

        //Get Music By Id
        public async Task<Musicc> GetMusicByIdAsync(string id)
        {
            return await _musicCollection.Find(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync();
        }


        //Update Music By Id
        public async Task UpdateAsync(string id, Musicc model)
        {
            await _musicCollection.ReplaceOneAsync(x => x.Id == new ObjectId(id), model);
        }

        //Delete Music By Id
        public async Task DeleteMusicAsync(string id)
        {
            await _musicCollection.DeleteOneAsync(x => x.Id == new ObjectId(id));
        }

        //Get Count of Musics In Playlists
        public async Task<int> GetMusicListCountAsync(string musicDataId)
        {
            var filter = Builders<MusicToPlaylist>.Filter.Eq(m => m.musicDataId, musicDataId);
            var count = await _musictoPlaylistCollection.CountDocumentsAsync(filter);
            return (int)count;
        }

        //Fetch Music based On Playlist ID
        public async Task<List<MusicToPlaylist>> GetMusicByMusicDataId(string musicDataId)
        {
            var filter = Builders<MusicToPlaylist>.Filter.Eq(x => x.musicDataId, musicDataId);
            var results = await _musictoPlaylistCollection.Find(filter).ToListAsync();

            return results; // This will return a list of MusicToPlaylist documents matching the musicDataId
        }


    }


}
