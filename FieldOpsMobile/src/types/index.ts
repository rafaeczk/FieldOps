export interface WorkOrder {
  id: string;
  title: string;
  description: string;
  status: WorkOrderStatus;
  address: string;
  scheduledDate: string;
  operatorId: string;
  technicianId?: string;
  createdAt: string;
  updatedAt: string;
}

export enum WorkOrderStatus {
  Created = "Created",
  Assigned = "Assigned",
  InProgress = "InProgress",
  Completed = "Completed",
  Verified = "Verified",
}

export interface Report {
  id: string;
  workOrderId: string;
  note: string;
  photos: string[];
  latitude: number;
  longitude: number;
  signature?: string;
  qrData?: string;
  synced: boolean;
  createdAt: string;
}

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: "ADMIN" | "OPERATOR" | "TECHNICIAN";
}

export interface AuthResponse {
  token: string;
  user: User;
}
