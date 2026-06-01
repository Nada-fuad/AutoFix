import { useState ,useEffect} from "react";
import {
    Box,
    Button,
    Dialog,
    DialogContent,
    DialogTitle,
    Typography,
    Card,
    CardContent,
    Grid
} from "@mui/material";

import CreateCustomerForm from "../components/customers/CreateCustomerForm";


import { getCustomers } from "../api/customersApi"

function CustomerPage() {
    const [open, setOpen] = useState(false);
    const [customers, setCustomers] = useState<Customer[]>([]);

    useEffect(() => {
        async function loadCustomers() {
            const data = await getCustomers();
            setCustomers(data);
        }
        loadCustomers();
    }, []);

    return (
        <>
        <Box sx={{ p: 4 }}>
            <Typography variant="h4" sx={{ mb: 2, fontWeight: "bold" }}>
                Customers
            </Typography>

            <Button variant="contained" onClick={() => setOpen(true)}>
                + New Customer
            </Button>

            <Dialog
                open={open}
                onClose={() => setOpen(false)}
                maxWidth="md"
                fullWidth
            >
                <DialogTitle>Create New Customer</DialogTitle>

                <DialogContent>
                    <CreateCustomerForm />
                </DialogContent>
            </Dialog>
            </Box>

            <Grid container spacing={ 3} >
                {customers.map((customer) => (
                
                    <Grid size={{ xs: 12, md: 4 }} key={customer.customerId} sx={{ mb: 2 }}>
                        <Card sx={{height:"100%",borderRadius:3,boxShadow:3} }>
                        <CardContent>
                            <Typography variant="h6">{customer.name}</Typography>
                            <Typography color="text.secondary">{customer.email}</Typography>
                            <Typography color="text.secondary">{customer.phoneNumber}</Typography>
                            <Typography sx={{mt:1,fontWeight:"bold"} }>Vehicles</Typography>
                            {customer.vehicles.map((vehicle) => (

                                <Typography key={vehicle.vehicleId}>{vehicle.make} {vehicle.model}-{vehicle.year}-{ vehicle.licensePlate}</Typography>

                            )) }

                            </CardContent>
                        </Card>
                    </Grid>
                
                ))}

            </Grid>
        </>
    );
}

export default CustomerPage;