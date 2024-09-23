/* eslint-disable no-unused-vars */
// ContactUs.js
import React, { useState, useEffect } from 'react';
import './Login.css'; // Using Login.css

const Login = () => {

    useEffect(() => {
        // Apply style to <html>
        document.documentElement.style.overflow = 'hidden';

        return () => {
            // Reset style when the component unmounts
            document.documentElement.style.overflow = '';
        };
    }, []);


    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: ''
    });


    const [isLogin, setIsLogin] = useState(true); // State to control which form to display
    const [errorMessage, setErrorMessage] = useState(''); // State to store error messages

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();  // Prevent the default form submission

        // Clear previous error message
        setErrorMessage('');

        // Check if it's registration and passwords match
        if (!isLogin && formData.password !== formData.confirmPassword) {
            alert('Passwords do not match');
            return;
        }

        // Validate password length for both login and registration
        if (formData.password.length < 8) {
            alert('Password must be at least 8 characters long');
            return;
        }

        const url = isLogin
            ? 'https://localhost:7094/Member/Login' // Change this to your login endpoint
            : 'https://localhost:7094/Member/Registration'; // Change this to your registration endpoint

        fetch(url, {
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

                //CHeck if Email Already Exists
                if (data === "Exists") {
                    //document.getElementById("msg").style.color = "red";
                    //document.getElementById("msg").innerHTML = "Exists";
                    alert("Email Already Exists");
                }

                if (data === "NonExists") {
                    alert("Registered Successfully");
                }
               

                //Look for Admin Login
                if (data.id == "000000000000000000000000") {
                    sessionStorage.setItem('userId', data.id);
                    window.location.href = '/musiccrud';
                    alert("Login Successful");
                }

                //Look for User Login
                else if (data.id != null) {
                    alert(data.message);
                    sessionStorage.setItem('userId', data.id);
                    //document.getElementById("msg").style.color = data.color;
                    window.location.href = '/';
                }

                //Login Failed
                else {
                    document.getElementById("msg").style.color = "red";
                    document.getElementById("msg").innerHTML = "Invalid Credentials";
                }
                //setTimeout(() => {
                //    window.location.href = '/Index';
                //}, 2000);
            })
            .catch(error => console.error('Error sending message:', error));
    };


    return (
        <div className="background-container" style={{ overflow: 'hidden' }}>
            <div className="form-slider-container">
                {/* Slider Controls */}
                <div className="form-toggle">
                    <span onClick={() => setIsLogin(true)} className={isLogin ? 'active' : ''}>Login</span>
                    <span onClick={() => setIsLogin(false)} className={!isLogin ? 'active' : ''}>Register</span>
                </div>
                <div className={`slider ${isLogin ? 'login-active' : 'register-active'}`}>
                    {/* Login Form */}
                    <div className="form login-form">
                        <h1>Login</h1>
                        <form className="contact-form" onSubmit={handleSubmit}>
                            <label htmlFor="email">Email:</label>
                            <input
                                type="email"
                                id="email"
                                name="email"
                                placeholder="Your Email"
                                required
                                value={formData.email}
                                onChange={handleChange}
                            />

                            <label htmlFor="password">Password:</label>
                            <input
                                type="password"
                                id="password"
                                name="password"
                                placeholder="Your Password"
                                required
                                value={formData.password}
                                onChange={handleChange}
                            />

                            <button type="submit">Submit</button>
                        </form>
                        <div id="msg"></div>
                    </div>

                    {/* Registration Form */}
                    <div className="form register-form">
                        <h1>Registration</h1>
                        <div className="scrollable-form">
                            <form className="contact-form" onSubmit={handleSubmit}>
                                <label htmlFor="firstName">First Name:</label>
                                <input
                                    type="text"
                                    id="firstName"
                                    name="firstName"
                                    placeholder="Your First Name"
                                    required
                                    value={formData.firstName}
                                    onChange={handleChange}
                                />

                                <label htmlFor="lastName">Last Name:</label>
                                <input
                                    type="text"
                                    id="lastName"
                                    name="lastName"
                                    placeholder="Your Last Name"
                                    required
                                    value={formData.lastName}
                                    onChange={handleChange}
                                />

                                <label htmlFor="email">Email:</label>
                                <input
                                    type="email"
                                    id="email"
                                    name="email"
                                    placeholder="Your Email"
                                    required
                                    value={formData.email}
                                    onChange={handleChange}
                                />

                                <label htmlFor="password">Password:</label>
                                <input
                                    type="password"
                                    id="password"
                                    name="password"
                                    placeholder="Your Password"
                                    required
                                    value={formData.password}
                                    onChange={handleChange}
                                />

                                <label htmlFor="confirmPassword">Confirm Password:</label>
                                <input
                                    type="password"
                                    id="confirmPassword"
                                    name="confirmPassword"
                                    placeholder="Confirm Your Password"
                                    required
                                    value={formData.confirmPassword}
                                    onChange={handleChange}
                                />

                                {errorMessage && <div className="error-message" style={{ color: 'red' }}>{errorMessage}</div>}

                                <button type="submit">Submit</button>
                            </form>
                            <div id="msg"></div>
                        </div>
                    </div>
                </div>

               
            </div>
        </div>
    );
};

export default Login;
