using DataAccess.Models;
using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IMusicMapper
    {
        List<Musicc> Map(List<MusicDto> musicDtos);
        Musicc Map(MusicDto musicDtos);
    }

    public class MusicMapper : IMusicMapper
    {
        //Music
        public List<Musicc> Map(List<MusicDto> musicDtos)
        {
            var musicList = new List<Musicc>();  // List to store mapped objects
            foreach (var item in musicDtos)
            {
                var music = new Musicc();  // Create a new Musicc object for each item
                music.Year = item.Year;
                music.Title = item.Title;
                music.Genre = item.Genre;
                music.Artist = item.Artist;
                music.AudioUrl = item.AudioUrl;
                music.albumPhotoUrl = item.albumPhotoUrl;
                musicList.Add(music);  // Add the mapped object to the list
            }
            return musicList;  // Return the list of mapped objects
        }

        //Store Music
        public Musicc Map(MusicDto musicDtos)
        {
            var music = new Musicc()
            {
                Artist = musicDtos.Artist,
                AudioUrl = musicDtos.AudioUrl,
                albumPhotoUrl = musicDtos.albumPhotoUrl,
                Title = musicDtos.Title,
                Genre = musicDtos.Genre,
                Year = musicDtos.Year
            };
            return music;
        }

      
    }
}
