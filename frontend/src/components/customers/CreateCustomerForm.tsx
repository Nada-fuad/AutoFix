import { useState } from "react";
import type {
    CreateCustomerRequest,
    CreateVehicleRequest,
} from "../../types/customerTypes";
import { createCustomer } from "../../api/customersApi";
import { Box, Stack, TextField, Typography, Divider, Button } from "@mui/material";
import PersonIcon from "@mui/icons-material/Person";
function CreateCustomerForm() {
    const [form, setForm] = useState<CreateCustomerRequest>({
        name: "",
        email: "",
        phoneNumber: "",
        vehicles: [],
    });

    const [vehicle, setVehicle] = useState<CreateVehicleRequest>({
        make: "",
        model: "",
        year: new Date().getFullYear(),
        licensePlate: "",
    });

    const [message, setMessage] = useState("");
    const [customerSubmitted, setCustomerSubmitted] = useState(false);

    const [submitted, setSubmitted] = useState(false);
    function handleCustomerChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        
        setForm({
            ...form,
            [name]: value,
        });
    }

    function handleVehicleChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;

        setVehicle({
            ...vehicle,
            [name]: name === "year" ? Number(value) : value,
        });
    }

    function addVehicle() {
        setSubmitted(true);
        if (!vehicle.make || !vehicle.model || !vehicle.licensePlate) {
            return;
        }
        setForm({
            ...form,
            vehicles: [...form.vehicles, vehicle],
        });

        setVehicle({
            make: "",
            model: "",
            year: new Date().getFullYear(),
            licensePlate: "",
        });

        setSubmitted(false);
    }

    async function handleSubmit(event: React.FormEvent) {
        event.preventDefault();
        setCustomerSubmitted(true);
        if (!form.name || !form.email || !form.phoneNumber) {
            return;
        }
        try {
            await createCustomer(form);
            setMessage("Customer created successfully");

            setForm({
                name: "",
                email: "",
                phoneNumber: "",
                vehicles: [],
            });

            setCustomerSubmitted(false);

        } catch {
            setMessage("Error while creating customer");
        }

    }

    function removeVehicle(index: number) {
        setForm({
            ...form, vehicles: form.vehicles.filter((_, i) => i !== index),
  });
    }

    return (
        <Box onSubmit={handleSubmit} component="form">
            <Stack spacing={3 }>
            <Typography variant="h5" fontWeight="bold">Create Customer</Typography>

                <TextField
            label="Customer Name"
                name="name"
                placeholder="Name"
                value={form.name}
                    onChange={handleCustomerChange}
                    error={!form.name && customerSubmitted}
                    helperText={!form.name && customerSubmitted ? "Required" : ""}
                    margin="normal"
                fullWidth
            />
                <Stack direction="row" spacing={ 2}>
                    <TextField
                        label="Email"
                name="email"
                placeholder="Email"
                value={form.email}
                        onChange={handleCustomerChange}
                        error={!form.email && customerSubmitted}
                        helperText={!form.email && customerSubmitted ? "Required" : ""}
                    fullWidth
            />

                    <TextField
                        label="phoneNumber"
                name="phoneNumber"
                placeholder="Phone Number"
                value={form.phoneNumber}
                        onChange={handleCustomerChange}
                        error={!form.phoneNumber && customerSubmitted}
                        helperText={!form.phoneNumber && customerSubmitted ? "Required" : ""}
                    fullWidth
                />
                 </Stack>
            </Stack>
            <Divider sx={{my:3} }> Vehicle Information </Divider>
            <Stack direction="row" spacing={4}>
                <TextField
                    label="make"
                name="make"
                placeholder="Make"
                    value={vehicle.make}
                    error={!vehicle.make && submitted}
                    helperText={!vehicle.make&&submitted?"Required":"" }
                onChange={handleVehicleChange}
            />

                <TextField
            label="model"
                name="model"
                placeholder="Model"
                    value={vehicle.model}
                    error={!vehicle.model && submitted}
                    helperText={!vehicle.model && submitted ? "Required" : ""}
                onChange={handleVehicleChange}
            />

                <TextField
            label="year"
                name="year"
                type="number"
                placeholder="Year"
                    value={vehicle.year}
                    error={!vehicle.year && submitted}
                    helperText={!vehicle.year && submitted ? "Required" : ""}
                onChange={handleVehicleChange}
            />

                <TextField
                    label="licensePlate"
                name="licensePlate"
                placeholder="License Plate"
                    value={vehicle.licensePlate}
                    error={!vehicle.licensePlate && submitted}
                    helperText={!vehicle.licensePlate && submitted ? "Required" : ""}
                onChange={handleVehicleChange}
            />
            </Stack>
            <Box  sx={{display:"flex" ,justifyContent:"flex-end", mt:2} }>
            <Button type="button" variant="contained" onClick={addVehicle} sx={{ margin:"3" }}
>
                Add Vehicle
            </Button>
            </Box>
            <ul>
                {form.vehicles.map((v, index) => (
                   
                    <li key={index}>
                            {v.make} {v.model} - {v.year} - {v.licensePlate}

                            <Button type="button" variant="outlined" onClick={()=>removeVehicle(index)} sx={{ margin: "3" }}
                            >
                                Delete
                            </Button>
                    </li>
                    
                   
                ))}
            </ul>

            <Box  sx={{ display:"flex",justifyContent:"flex-end", mt: 3 }} >

                <Button startIcon={<PersonIcon /> }type="submit" variant="contained">

               

                    Create Customer</Button>
            </Box>
            <p>{message}</p>
        </Box>
    );
}

export default CreateCustomerForm;