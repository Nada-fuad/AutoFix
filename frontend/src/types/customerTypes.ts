export type Vehicle = {
    vehicleId: string;
    make: string;
    model: string;
    year: string;
    licensePlate: string;
};

export type Customer = {
    customerId: string;
    name: string;
    email: string;
    phoneNumber: string;
    vehicles: Vehicle[];
};


export type CreateVehicleRequest = {
    make: string;
    model: string;
    year: number;
    licensePlate: string;
};

export type CreateCustomerRequest = {
    name: string;
    email: string;
    phoneNumber: string;
    vehicles: CreateVehicleRequest[];
};



