// eslint-disable-next-line no-unused-vars
import React, { useState, useEffect } from "react";
import { useLocation } from 'react-router-dom';
import './Music.css';

const MyPlaylists = () => {
    const [musicList, setMusicList] = useState([]);
    const [searchQuery, setSearchQuery] = useState("");
    const [error, setError] = useState(null); // State for error handling

    const location = useLocation();
    const { playlistId } = location.state || {}; // Accessing the playlistId

    // Now you can use playlistId to fetch data or perform other actions
    //alert(playlistId); // For demonstration
   

    useEffect(() => {
        //const musicDataId = "66f01f4725507f92d4d5786e"; // Replace with the appropriate ID
        //const musicDataId = "66f0f563646a100104cd710d"; // Replace with the appropriate ID
        const musicDataId = playlistId; // Replace with the appropriate ID

        // Fetch music data from the backend
        const fetchMusicData = async () => {
            try {
                const response = await fetch(`https://localhost:7094/Music/getMusicByBasedonPlaylist/${musicDataId}`);
                if (!response.ok) {
                    throw new Error(`Error: ${response.status} - ${response.statusText}`);
                }
                const data = await response.json();
                setMusicList(data);
            } catch (error) {
                console.error("Error fetching music:", error);
                setError(error.message); // Set error message in state
            }
        };

        fetchMusicData();
    }, []);

    const filteredMusicList = musicList.filter((music) =>
        music.title.toLowerCase().includes(searchQuery.toLowerCase())
    );

    return (
        <div>
            <h1>My Playlists</h1>
            {error && <p className="error-message">{error}</p>} {/* Display error message */}
            <input
                type="text"
                placeholder="Search by title..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="search-input"
            />
            <div className="music-grid">
                {filteredMusicList.length > 0 ? (
                    filteredMusicList.map((music) => (
                        <div key={music.id} className="music-item">
                            <img
                                src={music.albumPhotoUrl}
                                alt={`${music.title} Album`}
                                className="album-cover"
                            />
                            <h2 className="music-title">{music.title}</h2>
                            <p className="music-artist">{music.artist}</p>
                            <p className="music-genre">{music.genre} ({music.year})</p>
                            <audio controls src={music.audioUrl}>
                                Your browser does not support the audio element.
                            </audio>
                        </div>
                    ))
                ) : (
                    <p>No music found</p>
                )}
            </div>
        </div>
    );
};

export default MyPlaylists;
