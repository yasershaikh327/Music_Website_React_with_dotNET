/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from 'react';

const ErrorPage = () => {
    const [errorMessage, setErrorMessage] = useState('');

    useEffect(() => {
        fetch('https://localhost:7094/Error/GetError')
            .then((response) => {
                if (!response.ok) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .catch((error) => {
                setErrorMessage(error.message);
                // Optionally redirect to an error page or show a modal
            });
    }, []);

    return (
        <div>
            {errorMessage && <div>Error: {errorMessage}</div>}
            {/* Other component content */}
        </div>
    );
};

export default ErrorPage;
