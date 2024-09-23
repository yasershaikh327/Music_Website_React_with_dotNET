using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IPlaylistMapper
    {
        Playlist Map(PlaylistDto playlistdto);
        List<Playlist> Map(List<PlaylistDto> playlistdto);
    }

    public class PlaylistMapper : IPlaylistMapper
    {
        //Add Single Playlists
        public Playlist Map(PlaylistDto playlistdto)
        {
            var playlist = new Playlist()
            {
                Description = playlistdto.Description,
                NameofPlayList = playlistdto.Name,
                userid = playlistdto.userid,    
            };
            return playlist;
        }

        //Get List of Playlists
        public List<Playlist> Map(List<PlaylistDto> playlistdto)
        {
            var playlistt = new List<Playlist>();
            foreach (var item in playlistdto)
            {
                var playlist = new Playlist();
                playlist.NameofPlayList = item.Name;
                playlist.Description = item.Description;
                playlist.userid = item.userid;
                playlistt.Add(playlist);
            }
            return playlistt;
        }

    }
}
