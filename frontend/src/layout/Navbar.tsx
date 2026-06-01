import {
    AppBar,
    Toolbar,
    Typography,
 
    Button,
    Stack,
} from "@mui/material";
import { Link } from "react-router-dom";
type NavbarProps = {
    isLoggedIn: boolean;
};

function Navbar({ isLoggedIn }: NavbarProps) {
    return (
        <AppBar position="static" sx={{ bgcolor: "#101815" }}>
            <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>

                <Typography variant="h6" sx={{ fontWeight: "bold" }}>
                    🚗 AutoFix
                </Typography>

                {isLoggedIn ? (
                    <Stack direction="row" spacing={3} alignItems="center">
                        <Button color="inherit" component={ Link} to="/customers">Customers</Button>
                        <Button color="inherit" component={Link } to="/repair-tasks">Services</Button>
                        <Button color="inherit">Work Orders</Button>
                        <Button color="inherit">Schedules</Button>

                        <Typography variant="body2">
                            me@localhost
                        </Typography>

                        <Button color="inherit">
                            Logout
                        </Button>
                    </Stack>
                ) : (
                    <Button color="inherit">
                        Login
                    </Button>
                )}

            </Toolbar>
        </AppBar>
    );
}

export default Navbar;