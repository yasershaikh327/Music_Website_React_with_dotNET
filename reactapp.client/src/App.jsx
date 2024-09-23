/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars, faTimes } from '@fortawesome/free-solid-svg-icons';
import ContactUs from "./components/ContactUs";
import HomePage from "./components/HomePage";
import AboutUs from "./components/AboutUs";
import Music from "./components/Music";
import './App.css';
import Login from "./components/Login";
import MusicCrud from "./components/MusicCrud";
import ErrorPage from "./components/Error";
import Playlist from "./components/Playlist";
import Pop from "./components/Popup";
import MyPlaylists from "./components/MyPlaylists";


const App = () => {
    const [menuOpen, setMenuOpen] = useState(false);
    const [userId, setUserId] = useState(null); // State to hold session userId

    const toggleMenu = () => {
        setMenuOpen(!menuOpen);
    };

    // Check for userId in session storage on component mount
    useEffect(() => {
        const storedUserId = sessionStorage.getItem('userId');
        setUserId(storedUserId);
    }, []);

    // Function to handle logout and remove userId from session
    const handleLogout = () => {
        sessionStorage.removeItem('userId'); // Remove userId from session storage
        setUserId(null); // Update state to reflect user is logged out
        alert('Logged out successfully');
        window.location.href = "/";
    };

    return (
        <Router>
            <div className="app">
                <header>
                    <nav>
                        <div className="logo">
                            <h1 id="Logos" style={{ textAlign: "left" }}>MuSic</h1>
                        </div>
                        <div className="hamburger" onClick={toggleMenu}>
                            <FontAwesomeIcon icon={menuOpen ? faTimes : faBars} />
                        </div>
                        <ul className={menuOpen ? "nav-links open" : "nav-links"}>
                            {/* Show these links to everyone (logged in or not) */}
                            <li onClick={() => setMenuOpen(false)}>
                                <Link to="/">Home</Link>
                            </li>

                            {userId ? (
                                <>
                                    {/* Links for logged-in users */}
                                    <li onClick={() => setMenuOpen(false)}>
                                        <Link to="/music">Music</Link>
                                    </li>
                                    <li onClick={() => setMenuOpen(false)}>
                                        <Link to="/playlist">Playlist</Link>
                                    </li>
                                    {/*Only for Admin*/ }
                                    {sessionStorage.getItem('userId') === "000000000000000000000000" && (
                                        <>
                                            <li onClick={() => setMenuOpen(false)}>
                                                <Link to="/musiccrud">Manage Musics</Link>
                                            </li>
                                        </>
                                    )}
                                    <li onClick={() => setMenuOpen(false)}>
                                        <Link onClick={handleLogout}>Logout</Link>
                                    </li>
                                </>
                            ) : (
                                <>
                                    {/* Links for guests */}
                                    <li onClick={() => setMenuOpen(false)}>
                                        <Link to="/about">About Us</Link>
                                    </li>
                                    <li onClick={() => setMenuOpen(false)}>
                                        <Link to="/contact">Contact Us</Link>
                                    </li>
                                    <li className="login-link" onClick={() => setMenuOpen(false)}>
                                        <Link to="/login">Login</Link>
                                    </li>
                                </>
                            )}
                        </ul>
                    </nav>
                </header>

                <main>
                    <Routes>
                        <Route path="/music" element={<Music />} />
                        <Route path="/" element={<HomePage />} />
                        <Route path="/about" element={<AboutUs />} />
                        <Route path="/contact" element={<ContactUs />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/musiccrud" element={<MusicCrud />} />
                        <Route path="/error" element={<ErrorPage />} />
                        <Route path="/playlist" element={<Playlist />} />
                        <Route path="/Pop" element={<Pop />} />
                        <Route path="/details" element={<MyPlaylists />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
};

export default App;
