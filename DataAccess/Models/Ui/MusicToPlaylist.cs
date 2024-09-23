using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Ui
{
    public class MusicToPlaylist
    {
        [BsonId] // Marks this property as the document's Id
        public ObjectId Id { get; set; }

        [BsonElement("musicListId")]
        public string musicListId { get; set; }

        [BsonElement("musicDataId")]
        public string musicDataId { get; set; }


        public class MusicToPlaylistList
        {
            [BsonId] // Marks this property as the document's Id
            public ObjectId Id { get; set; }

            [BsonElement("musicListId")]
            public string musicListId { get; set; }

            [BsonElement("musicDataId")]
            public string musicDataId { get; set; }

        }
    }
}
