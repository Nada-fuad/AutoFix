import { useState } from "react";
import { createCustomer } from "./customersApi";
import type { CreateCustomerRequest } from "./customerTypes";

function CustomerForm() {
  const [form, setForm] = useState<CreateCustomerRequest>({
    name: "",
    email: "",
    phoneNumber: "",
    vehicles: [],
  });

  const [message, setMessage] = useState("");

  function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
    setForm({
      ...form,
      [event.target.name]: event.target.value,
    });
  }

  async function handleSubmit(event: React.FormEvent) {
    event.preventDefault();

    try {
      await createCustomer(form);

      setMessage("Customer created successfully");

      setForm({
        name: "",
        email: "",
        phoneNumber: "",
      });
    } catch {
      setMessage("Something went wrong");
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>Create Customer</h2>

      <input
        name="name"
        placeholder="Name"
        value={form.name}
        onChange={handleChange}
      />

      <input
        name="email"
        placeholder="Email"
        value={form.email}
        onChange={handleChange}
      />

      <input
        name="phoneNumber"
        placeholder="Phone number"
        value={form.phoneNumber}
        onChange={handleChange}
      />

      <button type="submit">Create</button>

      <p>{message}</p>
    </form>
  );
}

export default CustomerForm;
