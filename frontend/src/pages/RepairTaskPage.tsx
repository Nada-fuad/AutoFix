 import CreateRepairTaskForm from "../components/customers/CreateRepairTaskForm"
import { useState } from "react";
import {
    Box,
    Button,
    Dialog,
    DialogContent,
    DialogTitle,
    Typography,
} from "@mui/material";

function RepairTaskPage() {

    const [open, setOpen] = useState(false);

    return <Box sx={{ p: 4}}>
        <Typography variant="h4" sx={{ mb: 2, fontWeight: "bold" }}>

            RepairTask
        </Typography>

        <Button variant="contained"  onClick={() => setOpen(true)}>
            + New RepairTask
        </Button>

        <Dialog
            open={open}
            onClose={() => setOpen(false)}
            maxWidth="md"
            fullWidth
        >
            <DialogTitle>Create New RepairTask</DialogTitle>

            <DialogContent>

                  <CreateRepairTaskForm />
                </DialogContent>
            </Dialog>
        </Box>
            ;
}

export default  RepairTaskPage;