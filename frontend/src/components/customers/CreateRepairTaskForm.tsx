import type { CreateRepairTaskRequest, CreatePartRequest } from "../../types/rapairTaskType";
import { useState } from "react";
import {createRepairTask} from "../../api/repairTasksApi"


function CreateRepairTaskForm() {


    const [form, setForm] = useState<CreateRepairTaskRequest>({
        name: "",
        estimatedDurationInMins: 0,
        laborCost:0,
        parts:[],
    });

    const [part, setPart] = useState<CreatePartRequest>({
        name: "",
        cost: 0,
        quantity: 0,
    });
    const [message, setMessage] = useState("");

    function handleTaskChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;

        setForm({
            ...form,
            [name]: name === "name" ? value : Number(value),
        });
    }

    function handlePartChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;

        setPart({
            ...part,
            [name]: name === "name" ? value : Number(value),
        });
    }

    function addPart() {
        setForm({
            ...form,
            parts: [...form.parts, part],
        });

        setPart({
            name: "",
            cost: 0,
            quantity: 1,
        });
    }

    async function handleSubmit(event: React.FormEvent) {
        event.preventDefault();

        try {
            await createRepairTask(form);

            setMessage("Repair task created successfully");

            setForm({
                name: "",
                laborCost: 0,
                estimatedDurationInMins: 0,
                parts: [],
            });
        } catch {
            setMessage("Error while creating repair task");
        }
    }


    return (
        <form onSubmit={handleSubmit}>
            <h2>Create Repair Task</h2>

            <input
                name="name"
                placeholder="Task Name"
                value={form.name}
                onChange={handleTaskChange}
            />

            <input
                name="laborCost"
                type="number"
                placeholder="Labor Cost"
                value={form.laborCost}
                onChange={handleTaskChange}
            />

            <input
                name="estimatedDurationInMins"
                type="number"
                placeholder="Duration in minutes"
                value={form.estimatedDurationInMins}
                onChange={handleTaskChange}
            />

            <h3>Add Part</h3>

            <input
                name="name"
                placeholder="Part Name"
                value={part.name}
                onChange={handlePartChange}
            />

            <input
                name="cost"
                type="number"
                placeholder="Part Cost"
                value={part.cost}
                onChange={handlePartChange}
            />

            <input
                name="quantity"
                type="number"
                placeholder="Quantity"
                value={part.quantity}
                onChange={handlePartChange}
            />

            <button type="button" onClick={addPart}>
                Add Part
            </button>

            <ul>
                {form.parts.map((p, index) => (
                    <li key={index}>
                        {p.name} - {p.cost} € - Qty: {p.quantity}
                    </li>
                ))}
            </ul>

            <button type="submit">Save Repair Task</button>

            <p>{message}</p>
        </form>
    );
}

export default CreateRepairTaskForm;