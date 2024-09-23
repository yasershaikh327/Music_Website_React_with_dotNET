using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Models.Ui
{
    public class Musicc
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
        [BsonIgnoreIfNull]
        public string? albumPhotoUrl { get; set; }

        [BsonElement("AudioUrl")]
        [BsonIgnoreIfNull]
        public string? AudioUrl { get; set; }


    }


    public class DisplayMusicc
    {
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("artist")]
        public string Artist { get; set; }

        [BsonElement("genre")]
        public string Genre { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("albumPhotoUrl")]
        [BsonIgnoreIfNull]
        public string? albumPhotoUrl { get; set; }

        [BsonElement("AudioUrl")]
        [BsonIgnoreIfNull]
        public string? AudioUrl { get; set; }


    }

}
