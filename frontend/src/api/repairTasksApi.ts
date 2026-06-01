import type{ CreateRepairTaskRequest } from "../types/repairTaskType";

const API_URL = "https://localhost:7291/api/repair-tasks";

export async function createRepairTask(request: CreateRepairTaskRequest) {
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(request),
    });

    if (!response.ok) {
        const errorText = await response.text();
        console.log("API error:", errorText);
        throw new Error(errorText);
    }

    return response.json();
}
