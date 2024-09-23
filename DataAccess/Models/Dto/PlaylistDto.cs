using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Ui
{
    public class PlaylistDto
    {
        [BsonId] // Marks this property as the document's Id
        public ObjectId Id { get; set; }

        [BsonElement("userid")]
        public string userid { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }

    public class PlaylistDtos
    {
        public PlaylistDto playlistDto { get; set; }
        public PlaylistDtos(PlaylistDto PlaylistDto) 
        { 
            playlistDto = PlaylistDto;
        }
    }
}
