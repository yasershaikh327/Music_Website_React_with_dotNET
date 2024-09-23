/* eslint-disable no-unused-vars */
// ContactUs.js
import React, { useState } from 'react';
import './ContactUs.css';

const ContactUs = () => {
    const [formData, setFormData] = useState({
        name: '',
        email: '',
        message: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();  // Prevent the default form submission

        fetch('https://localhost:7094/Member/Contact', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData),  // Sends form data as JSON
        })
            .then(response => response.json())
            .then(data => {
                document.getElementById("msg").innerHTML = data.message;
                console.log('Message sent:', data);  // Handle success
                setTimeout(() => {
                    window.location.href = '/Index';
                }, 2000);
            })
            .catch(error => console.error('Error sending message:', error)); // Fixed typo here

    };

    return (
        <>
            <div className="contact-container">
                <h1>Contact Us</h1>
                <div>
                    <form className="contact-form" onSubmit={handleSubmit}>
                        <label htmlFor="name">Name:</label>
                        <input type="text" id="name" name="name" placeholder="Your Name" required aria-label="Name" value={formData.name}
                            onChange={handleChange} />

                        <label htmlFor="email">Email:</label>
                        <input type="email" id="email" name="email" placeholder="Your Email" required aria-label="Email" value={formData.email}
                            onChange={handleChange} />

                        <label htmlFor="message">Message:</label>
                        <textarea id="message" name="message" placeholder="Your Message" required aria-label="Message" value={formData.message}
                            onChange={handleChange} ></textarea>

                        <button type="submit">Submit</button>
                    </form>
                    <div id="msg"></div> {/* Add this div to display messages */}
                </div>
            </div>
        </>
    );
};

export default ContactUs;
