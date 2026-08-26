import { create } from "zustand";
import { Report } from "../types";

interface OfflineState {
  pendingReports: Report[];
  pendingStatusUpdates: { workOrderId: string; status: string }[];
  addPendingReport: (report: Report) => void;
  addPendingStatusUpdate: (workOrderId: string, status: string) => void;
  clearPending: () => void;
}

export const useOfflineStore = create<OfflineState>((set) => ({
  pendingReports: [],
  pendingStatusUpdates: [],
  addPendingReport: (report) =>
    set((state) => ({
      pendingReports: [...state.pendingReports, report],
    })),
  addPendingStatusUpdate: (workOrderId, status) =>
    set((state) => ({
      pendingStatusUpdates: [
        ...state.pendingStatusUpdates,
        { workOrderId, status },
      ],
    })),
  clearPending: () => set({ pendingReports: [], pendingStatusUpdates: [] }),
}));
