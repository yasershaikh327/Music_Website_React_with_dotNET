/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from "react";
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Button,
    TextField,
    Grid,
    Box,
    Typography
} from "@mui/material";
import "./MusicCrud.css"; // Customize the styles here

const MusicCrud = () => {
    const [musicData, setMusicData] = useState([]);
    const [editingIndex, setEditingIndex] = useState(null);
    const [errorMessage, setErrorMessage] = useState("");
    const [newMusic, setNewMusic] = useState({
        title: "",
        artist: "",
        genre: "",
        year: "",
        albumPhoto: null,
        audioFile: null
    });

    useEffect(() => {
        fetch("https://localhost:7094/Music/GetMusic")
            .then((response) => response.json())
            .then((data) => {
                const formattedData = data.map((item) => ({
                    id: item.id || 0,
                    title: item.title || "",
                    artist: item.artist || "",
                    genre: item.genre || "",
                    year: item.year || 0,
                    albumPhotoUrl: item.albumPhotoUrl || "",
                    audioUrl: item.audioUrl || ""
                }));
                setMusicData(formattedData);
            })
            .catch((error) => console.error("Error fetching music:", error));
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewMusic((prev) => ({ ...prev, [name]: value }));
    };

    const handleFileChange = (e) => {
        const { name, files } = e.target;
        setNewMusic((prev) => ({ ...prev, [name]: files[0] }));
    };

    const addMusic = async (e) => {
        e.preventDefault();
        if (newMusic.albumPhoto && newMusic.audioFile) {
            const formData = new FormData();
            formData.append("title", newMusic.title);
            formData.append("artist", newMusic.artist);
            formData.append("genre", newMusic.genre);
            formData.append("year", newMusic.year);
            formData.append("albumPhoto", newMusic.albumPhoto);
            formData.append("audioFile", newMusic.audioFile);

            try {
                const response = await fetch("https://localhost:7094/Music/AddMusic", {
                    method: "POST",
                    body: formData
                });

                if (response.ok) {
                    const newEntry = await response.json();
                    setMusicData((prev) => [...prev, newEntry]);
                    resetForm();
                } else {
                    const errorText = await response.text();
                    setErrorMessage(`Failed to add music: ${errorText}`);
                }
            } catch (error) {
                setErrorMessage(`Error adding music: ${error.message}`);
            }
        }
        //Refresh page to see new Added Data
        window.location.reload(false);
    };

    const deleteMusic = async (id) => {
        try {
            const response = await fetch(`https://localhost:7094/Music/DeleteMusic/${id}`, {
                method: "DELETE"
            });

            if (response.ok) {
                setMusicData((prev) => prev.filter((music) => music.id !== id));
            } else {
                const errorResponse = await response.json();
                if (errorResponse && errorResponse.message) {
                    setErrorMessage(`Failed to delete music: ${errorResponse.message}`);
                } else {
                    const errorText = await response.text();
                    setErrorMessage(`Failed to delete music: ${errorText}`);
                }
            }
        } catch (error) {
            setErrorMessage(`Error deleting music: ${error.message}`);
        }
    };

    const updateMusic = async (e, index) => {
        e.preventDefault();
        const musicToUpdate = musicData[index];
        const musicId = musicToUpdate.id;

        const updatedData = {
            title: musicToUpdate.title,
            artist: musicToUpdate.artist,
            genre: musicToUpdate.genre,
            year: musicToUpdate.year
        };

        try {
            const response = await fetch(`https://localhost:7094/Music/UpdateMusic/${musicId}`, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(updatedData)
            });

            if (response.ok) {
                const updatedMusic = await response.json();
                setMusicData((prev) =>
                    prev.map((music, idx) => (idx === index ? updatedMusic : music))
                );
                setEditingIndex(null);
            } else {
                const errorText = await response.text();
                setErrorMessage(`Failed to update music: ${errorText}`);
            }
        } catch (error) {
            setErrorMessage(`Error updating music: ${error.message}`);
        }
    };

    const resetForm = () => {
        setNewMusic({ title: "", artist: "", genre: "", year: "", albumPhoto: null, audioFile: null });
        setErrorMessage("");
    };

    return (
        <div className="container">
            <Typography variant="h3" gutterBottom style={{ color: "lawngreen", textAlign: "center" }}>
                <h1 style={{ fontFamily: 'Pacifico, cursive'}}>Music is an Art</h1>
            </Typography>
            {errorMessage && <Typography color="error">{errorMessage}</Typography>}

            <TableContainer component={Paper} className="table-container">
                <Table aria-label="music table">
                    <TableHead>
                        <TableRow>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Title</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Artist</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Genre</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Year</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Album Photo</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Audio</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {musicData.map((music, index) => (
                            <TableRow key={index}>
                                <TableCell>
                                    {editingIndex === index ? (
                                        <TextField
                                            name="title"
                                            value={music.title}
                                            onChange={(e) => {
                                                const updatedMusic = [...musicData];
                                                updatedMusic[index].title = e.target.value;
                                                setMusicData(updatedMusic);
                                            }}
                                        />
                                    ) : (
                                        music.title
                                    )}
                                </TableCell>
                                <TableCell>
                                    {editingIndex === index ? (
                                        <TextField
                                            name="artist"
                                            value={music.artist}
                                            onChange={(e) => {
                                                const updatedMusic = [...musicData];
                                                updatedMusic[index].artist = e.target.value;
                                                setMusicData(updatedMusic);
                                            }}
                                        />
                                    ) : (
                                        music.artist
                                    )}
                                </TableCell>
                                <TableCell>
                                    {editingIndex === index ? (
                                        <TextField
                                            name="genre"
                                            value={music.genre}
                                            onChange={(e) => {
                                                const updatedMusic = [...musicData];
                                                updatedMusic[index].genre = e.target.value;
                                                setMusicData(updatedMusic);
                                            }}
                                        />
                                    ) : (
                                        music.genre
                                    )}
                                </TableCell>
                                <TableCell>{music.year}</TableCell>
                                <TableCell>
                                    <img src={music.albumPhotoUrl} alt="Album" width="50" />
                                </TableCell>
                                <TableCell>
                                    <audio controls src={music.audioUrl}>
                                        Your browser does not support the audio element.
                                    </audio>
                                </TableCell>
                                <TableCell>
                                    <div className="action-buttons">
                                        <Button
                                            variant="contained"
                                            color="secondary"
                                            onClick={() => deleteMusic(music.id)}
                                        >
                                            Delete
                                        </Button>
                                        {editingIndex === index ? (
                                            <Button
                                                variant="contained"
                                                color="primary"
                                                onClick={(e) => updateMusic(e, index)}
                                            >
                                                Save
                                            </Button>
                                        ) : (
                                            <Button
                                                variant="contained"
                                                color="primary"
                                                onClick={() => setEditingIndex(index)}
                                            >
                                                Edit
                                            </Button>
                                        )}
                                    </div>
                                </TableCell>

                                {/*<TableCell>*/}
                                {/*    <Button*/}
                                {/*        variant="contained"*/}
                                {/*        color="secondary"*/}
                                {/*        onClick={() => deleteMusic(music.id)}*/}
                                {/*    >*/}
                                {/*        Delete*/}
                                {/*    </Button>*/}
                                {/*    {editingIndex === index ? (*/}
                                {/*        <Button*/}
                                {/*            variant="contained"*/}
                                {/*            color="primary"*/}
                                {/*            onClick={(e) => updateMusic(e, index)}*/}
                                {/*        >*/}
                                {/*            Save*/}
                                {/*        </Button>*/}
                                {/*    ) : (*/}
                                {/*        <Button*/}
                                {/*            variant="contained"*/}
                                {/*                color="primary"*/}
                                {/*                style={{ width: '-webkit-fill-available' } }*/}
                                {/*            onClick={() => setEditingIndex(index)}*/}
                                {/*        >*/}
                                {/*            Edit*/}
                                {/*        </Button>*/}
                                {/*    )}*/}
                                {/*</TableCell>*/}
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Box mt={4}>
                <Typography variant="h4" gutterBottom style={{ color: "lawngreen", fontFamily: 'Pacifico, cursive'}}>
                    Add New Music
                </Typography>
                <form className="add-music-form" onSubmit={addMusic}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                name="title"
                                label="Title"
                                value={newMusic.title}
                                onChange={handleInputChange}
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                name="artist"
                                label="Artist"
                                value={newMusic.artist}
                                onChange={handleInputChange}
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                name="genre"
                                label="Genre"
                                value={newMusic.genre}
                                onChange={handleInputChange}
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                name="year"
                                label="Year"
                                type="number"
                                value={newMusic.year}
                                onChange={handleInputChange}
                                fullWidth
                                required
                                inputProps={{ maxLength: 4 }}
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Button
                                variant="outlined"
                                component="label"
                                fullWidth
                                style={{
                                    justifyContent: 'flex-start', // Align text to left
                                    textTransform: 'none' // Prevent text from being uppercase
                                }}
                            >
                                Upload Album Photo
                                <input
                                    type="file"
                                    name="albumPhoto"
                                    accept="image/*"
                                    onChange={handleFileChange}
                                    style={{ display: "none" }} // Hide the default file input
                                />
                            </Button>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Button
                                variant="outlined"
                                component="label"
                                fullWidth
                                style={{
                                    justifyContent: 'flex-start', // Align text to left
                                    textTransform: 'none' // Prevent text from being uppercase
                                }}
                            >
                                Upload Song
                                <input
                                    type="file"
                                    name="audioFile"
                                    accept="audio/*"
                                    onChange={handleFileChange}
                                    style={{ display: "none" }}
                                    required
                                />
                            </Button>
                        </Grid>
                    </Grid>
                    <Box mt={2}>
                        <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            fullWidth
                            className="add-button"
                        >
                            Add Music
                        </Button>
                    </Box>
                </form>
            </Box>
        </div>
    );
};

export default MusicCrud;
