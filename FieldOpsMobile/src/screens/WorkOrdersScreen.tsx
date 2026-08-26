import React from "react";
import {
  View,
  Text,
  FlatList,
  TouchableOpacity,
  StyleSheet,
} from "react-native";
import { WorkOrder, WorkOrderStatus } from "../types";

const MOCK_WORK_ORDERS: WorkOrder[] = [
  {
    id: "1",
    title: "AC Repair",
    description: "Air conditioning unit not cooling",
    status: WorkOrderStatus.Assigned,
    address: "123 Main St, Warsaw",
    scheduledDate: "2026-08-27",
    operatorId: "op1",
    technicianId: "tech1",
    createdAt: "2026-08-26",
    updatedAt: "2026-08-26",
  },
  {
    id: "2",
    title: "Boiler Maintenance",
    description: "Annual boiler inspection",
    status: WorkOrderStatus.Created,
    address: "456 Oak Ave, Krakow",
    scheduledDate: "2026-08-28",
    operatorId: "op1",
    createdAt: "2026-08-26",
    updatedAt: "2026-08-26",
  },
];

const STATUS_COLORS: Record<WorkOrderStatus, string> = {
  [WorkOrderStatus.Created]: "#6b7280",
  [WorkOrderStatus.Assigned]: "#2563eb",
  [WorkOrderStatus.InProgress]: "#f59e0b",
  [WorkOrderStatus.Completed]: "#10b981",
  [WorkOrderStatus.Verified]: "#8b5cf6",
};

export default function WorkOrdersScreen() {
  const renderItem = ({ item }: { item: WorkOrder }) => (
    <TouchableOpacity style={styles.card}>
      <View style={styles.cardHeader}>
        <Text style={styles.cardTitle}>{item.title}</Text>
        <View
          style={[
            styles.statusBadge,
            { backgroundColor: STATUS_COLORS[item.status] },
          ]}
        >
          <Text style={styles.statusText}>{item.status}</Text>
        </View>
      </View>
      <Text style={styles.cardDescription}>{item.description}</Text>
      <Text style={styles.cardAddress}>{item.address}</Text>
      <Text style={styles.cardDate}>Scheduled: {item.scheduledDate}</Text>
    </TouchableOpacity>
  );

  return (
    <View style={styles.container}>
      <FlatList
        data={MOCK_WORK_ORDERS}
        renderItem={renderItem}
        keyExtractor={(item) => item.id}
        contentContainerStyle={styles.list}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f5f5f5",
  },
  list: {
    padding: 16,
  },
  card: {
    backgroundColor: "#fff",
    borderRadius: 12,
    padding: 16,
    marginBottom: 12,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  cardHeader: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 8,
  },
  cardTitle: {
    fontSize: 18,
    fontWeight: "600",
    color: "#333",
  },
  statusBadge: {
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 12,
  },
  statusText: {
    color: "#fff",
    fontSize: 12,
    fontWeight: "600",
  },
  cardDescription: {
    fontSize: 14,
    color: "#666",
    marginBottom: 8,
  },
  cardAddress: {
    fontSize: 14,
    color: "#333",
    marginBottom: 4,
  },
  cardDate: {
    fontSize: 12,
    color: "#999",
  },
});
