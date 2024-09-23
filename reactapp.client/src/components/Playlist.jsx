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
import { useNavigate } from "react-router-dom";  // Import useNavigate for redirection
import "./Playlist.css"; // Customize the styles here

const Playlist = () => {
    const [musicData, setMusicData] = useState([]);
    const [editingIndex, setEditingIndex] = useState(null);
    const [errorMessage, setErrorMessage] = useState("");
    const [musicListCounts, setMusicListCounts] = useState([]);
    const [newMusic, setNewMusic] = useState({
        nameofplaylist: "",
        description: ""
    });

    const navigate = useNavigate();  // Hook for navigation
    useEffect(() => {
        const fetchPlaylists = async () => {
            try {
                const userId = sessionStorage.getItem("userId"); // Get the user ID from session storage

                if (!userId) {
                    console.error("User ID not found in session storage");
                    return; // Exit if no user ID
                }

                const response = await fetch(`https://localhost:7094/Member/GetAllMusicAsyncById/${userId}`);
                const data = await response.json();
                //alert(data);
                const formattedData = data.map((item) => ({
                    id: item.id || 0,
                    description: item.description || "",
                    nameofPlayList: item.nameofPlayList || ""
                }));

                setMusicData(formattedData);

                const musicListCounts = await Promise.all(formattedData.map(async (item) => {
                    const countResponse = await fetch(`https://localhost:7094/Music/getMusicListCount/${item.id}`);
                    const countData = await countResponse.json();
                    return { id: item.id, count: countData.count || 0 };
                }));

                setMusicListCounts(musicListCounts);

            } catch (error) {
                console.error("Error fetching music:", error);
            }
        };

        fetchPlaylists();
    }, []);

    //useEffect(() => {
    //    const fetchPlaylists = async () => {
    //        try {
    //            const response = await fetch("https://localhost:7094/Member/getplaylistt");
    //            const data = await response.json();
    //            alert(data);
    //            const formattedData = data.map((item) => ({
    //                id: item.id || 0,
    //                description: item.description || "",
    //                nameofPlayList: item.nameofPlayList || ""
    //            }));

    //            setMusicData(formattedData);

    //            const musicListCounts = await Promise.all(formattedData.map(async (item) => {
    //                const countResponse = await fetch(`https://localhost:7094/Music/getMusicListCount/${item.id}`);
    //                const countData = await countResponse.json();
    //                return { id: item.id, count: countData.count || 0 };
    //            }));

    //            setMusicListCounts(musicListCounts);

    //        } catch (error) {
    //            console.error("Error fetching music:", error);
    //        }
    //    };

    //    fetchPlaylists();
    //}, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewMusic((prev) => ({ ...prev, [name]: value }));
    };

    const addPlaylist = async (e) => {
        e.preventDefault();
        const newPlaylist = {
            nameofplaylist: newMusic.nameofplaylist,
            description: newMusic.description,
            userid: sessionStorage.getItem('userId')
        };

        try {
            const response = await fetch("https://localhost:7094/Member/addplaylistt", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newPlaylist)
            });

            if (response.ok) {
                const newEntry = await response.json();
                setMusicData((prev) => [...prev, newEntry]);
                resetForm();
                alert("Playlist Added Successfully");
                window.location.reload();
            } else {
                const errorText = await response.text();
                setErrorMessage(`Failed to add music: ${errorText}`);
            }
        } catch (error) {
            setErrorMessage(`Error adding music: ${error.message}`);
        }
    };

    const deletePlaylist = async (id) => {
        try {
            const response = await fetch(`https://localhost:7094/Member/DeletPlaylist/${id}`, {
                method: "DELETE"
            });

            if (response.ok) {
                setMusicData((prev) => prev.filter((music) => music.id !== id));
                alert("PlayList Deleted Successfully");
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
            nameofplaylist: musicToUpdate.nameofPlayList,
            description: musicToUpdate.description,
            userid: sessionStorage.getItem('userId')
        };

        try {
            const response = await fetch(`https://localhost:7094/Member/UpdatePlaylist/${musicId}`, {
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
                alert("Playlist Updated Successfully");
            } else {
                const errorText = await response.text();
                setErrorMessage(`Failed to update music: ${errorText}`);
            }
        } catch (error) {
            setErrorMessage(`Error updating music: ${error.message}`);
        }
    };

    const resetForm = () => {
        setNewMusic({ nameofplaylist: "", description: "" });
        setErrorMessage("");
    };

    // New function to handle secure state passing instead of exposing the ID in URL
    const handleNavigateToDetails = (id) => {
        navigate('/details', { state: { playlistId: id } });  // Passing id secretly using state
    };

    return (
        <div className="container">
            <Typography variant="h3" gutterBottom style={{ color: "lawngreen", textAlign: "center" }}>
                <h1 style={{ fontFamily: 'Pacifico, cursive' }}>Playlists</h1>
            </Typography>
            {errorMessage && <Typography color="error">{errorMessage}</Typography>}

            <TableContainer component={Paper} className="table-container">
                <Table aria-label="music table">
                    <TableHead>
                        <TableRow>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Name</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Description</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Navigate</TableCell>
                            <TableCell style={{ minWidth: '150px', padding: '10px' }}>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {musicData.map((music, index) => (
                            <TableRow key={music.id}>
                                <TableCell>
                                    {editingIndex === index ? (
                                        <TextField
                                            name="nameofPlayList"
                                            value={music.nameofPlayList}
                                            onChange={(e) => {
                                                const updatedMusic = [...musicData];
                                                updatedMusic[index].nameofPlayList = e.target.value;
                                                setMusicData(updatedMusic);
                                            }}
                                        />
                                    ) : (
                                        music.nameofPlayList
                                    )}
                                </TableCell>
                                <TableCell>
                                    {editingIndex === index ? (
                                        <TextField
                                            name="description"
                                            value={music.description}
                                            onChange={(e) => {
                                                const updatedMusic = [...musicData];
                                                updatedMusic[index].description = e.target.value;
                                                setMusicData(updatedMusic);
                                            }}
                                        />
                                    ) : (
                                        music.description
                                    )}
                                </TableCell>
                                <TableCell>
                                    <Button onClick={() => handleNavigateToDetails(music.id)}>Click Here</Button> {/* Secretly pass ID */}
                                </TableCell>
                                <TableCell>
                                    <div className="action-buttons">
                                        <Button
                                            variant="contained"
                                            color="secondary"
                                            onClick={() => deletePlaylist(music.id)}
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
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Box mt={4}>
                <Typography variant="h4" gutterBottom style={{ color: "lawngreen", fontFamily: 'Pacifico, cursive' }}>
                    Add New Playlist
                </Typography>
                <form className="add-music-form" onSubmit={addPlaylist}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                name="nameofplaylist"
                                label="Name of Playlist"
                                value={newMusic.nameofplaylist}
                                onChange={handleInputChange}
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                name="description"
                                label="Desc of Playlist"
                                value={newMusic.description}
                                onChange={handleInputChange}
                                fullWidth
                                required
                            />
                        </Grid>
                    </Grid>
                    <Button variant="contained" color="primary" type="submit" style={{ marginTop: "20px" }}>
                        Add
                    </Button>
                </form>
            </Box>
        </div>
    );
};

export default Playlist;
