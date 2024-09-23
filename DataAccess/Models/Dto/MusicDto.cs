using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Models.Dto
{
    public class MusicDto
    {
        [BsonId] // Marks this property as the document's Id
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("artist")]
        public string Artist { get; set; }

        [BsonElement("genre")]
        public string Genre { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("albumPhotoUrl")]
        public string albumPhotoUrl { get; set; }

        [BsonElement("AudioUrl")]
        public string AudioUrl { get; set; }


    }

    public class MusiccDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string AlbumPhotoUrl { get; set; }
        public string AudioUrl { get; set; }
    }

    public class DisplayMusicDto
    { 

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("artist")]
        public string Artist { get; set; }

        [BsonElement("genre")]
        public string Genre { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

 


    }
    public class MusicDtos
    {
        public MusicDto Dto { get; set; }
        public DisplayMusicDto DDto { get; set; }
        public MusicDtos(MusicDto music, DisplayMusicDto dDto)
        {
            music = new MusicDto();
            DDto = dDto;
        }
    }
}
