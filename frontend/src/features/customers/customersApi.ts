import type { CreateCustomerRequest } from "./customerTypes";

const API_URL = "https://localhost:7291/api/customers";

export async function createCustomer(customer: CreateCustomerRequest) {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(customer),
  });

  if (!response.ok) {
    const errorText = await response.text();
    console.log("API error:", errorText);
    throw new Error(errorText);
  }

  return response.json();
}
