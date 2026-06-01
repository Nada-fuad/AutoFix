import { BrowserRouter, Routes, Route } from "react-router-dom";
import CustomerPage from "./pages/CustomerPage";
import RepairTaskPage from "./pages/RepairTaskPage";
import HomePage from "./pages/HomePage";


import Navbar from "./layout/Navbar"
function App() {
    const isLoggedIn = true;
  return ( 
      <BrowserRouter>
          <Navbar isLoggedIn={isLoggedIn}/>
          <Routes>
              <Route path="/" element={<HomePage />}/>
              <Route path="/customers" element={<CustomerPage />}/>
              <Route path="/repair-tasks" element={<RepairTaskPage />}/>


          </Routes>
    </BrowserRouter>
  );
}

export default App;
