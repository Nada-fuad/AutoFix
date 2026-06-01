export type Part = {
    partId: string;
    name: string;
    cost: number;
    quantity: number;
};


export type RepairTask = {
    repairTaskId: string;
    name: string;
    estimatedDurationInMins: number;
    laborCost: number;
    totalCost: number;
    parts: Part[];
};



export type CreatePartRequest = {
    name: string;
    cost: number;
    quantity: number;
};


export type CreateRepairTaskRequest = {
    name: string;
    estimatedDurationInMins: number;
    laborCost: number;
    parts: CreatePartRequest[];
};