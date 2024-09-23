/* eslint-disable no-unused-vars */
import React, { useState } from 'react';
import { Button, Modal, Box, Typography } from '@mui/material';

// Popup Component
const PopupExample = () => {
    // State to manage modal visibility
    const [open, setOpen] = useState(false);

    // Functions to open and close the modal
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    return (
        <div>
            {/* Button to open the popup */}
            <Button variant="contained" color="primary" onClick={handleOpen}>
                Open Popup
            </Button>

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
                        Popup Title
                    </Typography>
                    <Typography sx={{ mt: 2 }}>
                        This is the content of the popup. You can add any components here.
                    </Typography>
                    <Button variant="outlined" onClick={handleClose} sx={{ mt: 2 }}>
                        Close Popup
                    </Button>
                </Box>
            </Modal>
        </div>
    );
};

export default PopupExample;
