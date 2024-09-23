using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IMusictoPlaylistMapper
    {
        MusicToPlaylist Map(DtoMusicToPlaylist dto);
        List<MusicToPlaylist> Map(List<DtoMusicToPlaylist> dto);
    }

    public class MusictoPlaylistMapper : IMusictoPlaylistMapper
    {
        //Add One Music to Playlist
        public MusicToPlaylist Map(DtoMusicToPlaylist dto)
        {
            var playlist = new MusicToPlaylist()
            {
                musicListId = dto.musicListId,
                musicDataId = dto.musicDataId,
            };
            return playlist;
        }

        //Fetch List of Music to Playlist
        public List<MusicToPlaylist> Map(List<DtoMusicToPlaylist> dtoList)
        {
            var playlist = new List<MusicToPlaylist>();
            foreach (var dto in dtoList)
            {
                var musicToPlaylist = new MusicToPlaylist()
                {
                    musicListId = dto.musicListId,
                    musicDataId = dto.musicDataId
                };

                playlist.Add(musicToPlaylist);
            }
            return playlist;
        }
    }
}
