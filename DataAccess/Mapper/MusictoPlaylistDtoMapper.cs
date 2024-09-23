using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IDtoMusictoPlaylistMapper
    {
        DtoMusicToPlaylist Map(MusicToPlaylist playlisttt);
        List<DtoMusicToPlaylist> Map(List<MusicToPlaylist> playlisttt);
    }

    public class MusictoPlaylistDtoMapper : IDtoMusictoPlaylistMapper
    {
        //Add One Dto Music to Playlist
        public DtoMusicToPlaylist Map(MusicToPlaylist playlisttt)
        {
            var playlistt = new DtoMusicToPlaylist()
            {
                musicListId = playlisttt.musicListId,
                musicDataId = playlisttt.musicDataId,
            };
            return playlistt;
        }

        //Fetch List of Dto Music to Playlist
        public List<DtoMusicToPlaylist> Map(List<MusicToPlaylist> playlisttt)
        {
            var dtoList = new List<DtoMusicToPlaylist>();

            foreach (var playlistItem in playlisttt)
            {
                var dto = new DtoMusicToPlaylist()
                {
                    musicListId = playlistItem.musicListId,
                    musicDataId = playlistItem.musicDataId
                };

                dtoList.Add(dto);
            }

            return dtoList;
        }

    }
}
