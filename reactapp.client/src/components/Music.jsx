/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from "react";
import { Button, Modal, Box, Typography } from '@mui/material';
import {
    Table,
    TableBody,
    TableCell,
    TableRow,
} from "@mui/material";
import './Music.css';

const Music = () => {
    // State to manage modal visibility and selected music ID
    const [open, setOpen] = useState(false);
    const [selectedMusicListId, setSelectedMusicListId] = useState(null);

    // State for music and playlists
    const [musicList, setMusicList] = useState([]);
    const [musicData, setMusicData] = useState([]);
    const [searchQuery, setSearchQuery] = useState("");
    const [, setSelectedItems] = useState({});

    // Fetch Music
    useEffect(() => {
        fetch("https://localhost:7094/Music/GetMusic")
            .then((response) => response.json())
            .then((data) => setMusicList(data))
            .catch((error) => console.error("Error fetching music:", error));
    }, []);

    // Fetch Playlists
    useEffect(() => {
        const userId = sessionStorage.getItem("userId"); // Get the user ID from session storage

        if (!userId) {
            console.error("User ID not found in session storage");
            return; // Exit if no user ID
        }

        fetch(`https://localhost:7094/Member/GetAllMusicAsyncById/${userId}`)
            .then((response) => response.json())
            .then((data) => {
                const formattedData = data.map((item) => ({
                    id: item.id || 0,
                    nameofPlayList: item.nameofPlayList || ""
                }));
                setMusicData(formattedData);
            })
            .catch((error) => console.error("Error fetching playlists:", error));
    }, []);

    // Filter musicList based on searchQuery
    const filteredMusicList = musicList.filter(music =>
        music.title.toLowerCase().includes(searchQuery.toLowerCase())
    );

    const handleOpen = (musicListId) => {
        setSelectedMusicListId(musicListId);
        setOpen(true);
    };

    const handleClose = () => setOpen(false);

    const handleAddToPlaylist = (musicDataId, musicListId) => {
        setSelectedItems((prevSelectedItems) => {
            const updatedItems = { ...prevSelectedItems };

            if (!updatedItems[musicListId]) {
                updatedItems[musicListId] = [];
            }

            if (updatedItems[musicListId].includes(musicDataId)) {
                updatedItems[musicListId] = updatedItems[musicListId].filter(id => id !== musicDataId);
                sendDataToAPI(musicDataId, musicListId, false); // Deselecting
            } else {
                updatedItems[musicListId].push(musicDataId);
                sendDataToAPI(musicDataId, musicListId, true); // Selecting
            }

            return updatedItems;
        });
    };

    //const sendDataToAPI = (musicDataId, musicListId, isSelected) => {
    //    const payload = {
    //        musicListId: musicListId,
    //        musicDataId: musicDataId,
    //        isSelected: isSelected
    //    };

    //    fetch('https://localhost:7094/Member/AddMusictoPlaylist', {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify(payload)
    //    })
    //        .then(response => response.json())
    //        .then(data => console.log('Success:', data))
    //        .catch((error) => console.error('Error:', error));
    //};
    const sendDataToAPI = (musicDataId, musicListId, isSelected) => {
        const payload = {
            musicListId: musicListId,
            musicDataId: musicDataId,
            isSelected: isSelected
        };

        fetch('https://localhost:7094/Member/AddMusictoPlaylist', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log('Success:', data);
                alert(data.message); // Alert on success
            })
            .catch((error) => {
                console.error('Error:', error);
                alert('Failed to add music to playlist.'); // Alert on error
            });
    };


    return (
        <div>
            <h1>Music List</h1>
            <input
                type="text"
                placeholder="Search by title..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="search-input"
            />
            <div className="music-grid">
                {filteredMusicList.map((music) => (
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
                        <Button variant="contained" color="primary" onClick={() => handleOpen(music.id)}>
                            Add to Playlist
                        </Button>
                    </div>
                ))}
            </div>

            {/* Modal component */}
            <Modal open={open} onClose={handleClose}>
                <Box
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        width: 400,
                        bgcolor: 'background.paper',
                        borderRadius: 2,
                        boxShadow: 24,
                        p: 4,
                    }}
                >
                    <Typography variant="h6" component="h2">
                        Select Playlist for Music
                    </Typography>
                    <Table sx={{ mt: 2 }}>
                        <TableBody>
                            {musicData.map((playlist) => (
                                <TableRow key={playlist.id}>
                                    <TableCell>{playlist.nameofPlayList}</TableCell>
                                    <TableCell align="right">
                                        <Button
                                            variant="contained"
                                            color="primary"
                                            onClick={() => handleAddToPlaylist(playlist.id, selectedMusicListId)}
                                        >
                                            Add +
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                    <Button variant="outlined" onClick={handleClose} sx={{ mt: 2 }}>
                        Close
                    </Button>
                </Box>
            </Modal>
        </div>
    );
};

export default Music;
