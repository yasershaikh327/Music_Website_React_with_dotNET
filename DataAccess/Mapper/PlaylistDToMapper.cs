using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IPlaylistDToMapper
    {
        PlaylistDto Map(Playlist playlist);
        List<PlaylistDto> Map(List<Playlist> playlist);
    }
    public class PlaylistDToMapper : IPlaylistDToMapper
    {
        public PlaylistDto Map(Playlist playlist)
        {

            var playlistdto = new PlaylistDto()
            {
                Description = playlist.Description,
                Name = playlist.NameofPlayList,
                userid = playlist.userid,
            };
            return playlistdto;
        }

        //Get List of Playlists
        public List<PlaylistDto> Map(List<Playlist> playlistdto)
        {
            var playlistt = new List<PlaylistDto>();
            foreach (var item in playlistdto)
            {
                var playlist = new PlaylistDto();
                playlist.Name = item.NameofPlayList;
                playlist.Description = item.Description;
                playlist.userid = item.userid;  
                playlistt.Add(playlist);
            }
            return playlistt;
        }
    }
}
