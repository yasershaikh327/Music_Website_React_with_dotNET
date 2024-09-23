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
    public interface IDtoMusicMapper
    {
        List<MusicDto> Map(List<Musicc> music);
        MusicDto Map(Musicc music);
    }

    public class MusicDtoMapper : IDtoMusicMapper
    {

        //Music
        public List<MusicDto> Map(List<Musicc> music)
        {
            var musicDtoList = new List<MusicDto>();  // List to store mapped objects
            foreach (var item in music)
            {
                var musicDto = new MusicDto();  // Create a new MusicDto for each item
                musicDto.Year = item.Year;
                musicDto.Title = item.Title;
                musicDto.Genre = item.Genre;
                musicDto.Artist = item.Artist;
                musicDto.AudioUrl = item.AudioUrl;
                musicDto.albumPhotoUrl = item.albumPhotoUrl;
                musicDtoList.Add(musicDto);  // Add the mapped object to the list
            }
            return musicDtoList;  // Return the list of mapped objects
        }

        //Store Music
        public MusicDto Map(Musicc music)
        {
            var dto = new MusicDto()
            {
                Artist = music.Artist,
                AudioUrl = music.AudioUrl,
                albumPhotoUrl = music.albumPhotoUrl,
                Title = music.Title,
                Genre = music.Genre,
                Year = music.Year
            };
            return dto;
        }

        
    }
}
