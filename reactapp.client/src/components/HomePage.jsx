/* eslint-disable no-unused-vars */
// HomePage.js
import { useEffect } from 'react';
import React from "react";
import './HomePage.css';

const HomePage = () => {

    useEffect(() => {
        // Apply style to <html>
        document.documentElement.style.overflow = 'hidden';

        return () => {
            // Reset style when the component unmounts
            document.documentElement.style.overflow = '';
        };
    }, []);


    return (
        <>
            <div className="homepage">
                <h1 id="title1">Welcome to the Music App</h1>
                <image src="../Images/HomepageImage2.jpg" />
            </div>
        </>
    );
};

export default HomePage;
