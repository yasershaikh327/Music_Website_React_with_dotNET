using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Ui
{
    public class Playlist
    {
        [BsonId] // Marks this property as the document's Id
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("userid")]
        public string userid { get; set; }

        [BsonElement("nameofplaylist")]
        public string NameofPlayList { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }

    //Read
    public class ReadPlaylist
    {
        public string Id { get; set; }
        //public string userid { get; set; }
        public string NameofPlayList { get; set; }
        public string Description { get; set; }
    }
}
