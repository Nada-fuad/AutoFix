export type CreateVehicleRequest = {
  make: string;
  model: string;
  year: string;
  licensePlate: string;
};

export type CreateCustomerRequest = {
  name: string;
  email: string;
  phoneNumber: string;
  vehicles: CreateVehicleRequest[];
};
