/* eslint-disable no-unused-vars */
import React from "react";
import './AboutUs.css';

const AboutUs = () => {
    return (
        <>
            <div className="about">
                <h1 style={{ color:'white'} }>About Us</h1>
                <p>This app is designed for music lovers to discover new tracks and artists.</p>
                <div className="flex-container">
                    <div className="flex-item image">
                        <video src="../Video/MusicVideo.mp4" alt="Description of the video" autoPlay loop />
                    </div>
                    <div className="flex-item text">
                        <h3>Music is a universal language that transcends boundaries and connects us all. It's the rhythm of life, a source of comfort, inspiration, and joy. Whether it's a soothing melody or an upbeat tune, music has the power to evoke emotions, bring back memories, and unite people from different cultures and walks of life. It speaks when words fail, heals when we're hurt, and amplifies moments of celebration. Through music, we find a reflection of our deepest feelings and an expression of our shared humanity. 🎶</h3>
                    </div>
                </div>
            </div>
        </>
    );
};

export default AboutUs;
