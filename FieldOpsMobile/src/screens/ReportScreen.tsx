import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  Alert,
  ScrollView,
} from "react-native";

export default function ReportScreen() {
  const [note, setNote] = useState("");

  const handleTakePhoto = () => {
    Alert.alert("Camera", "Camera integration coming soon");
  };

  const handleScanQR = () => {
    Alert.alert("QR Scanner", "QR scanner integration coming soon");
  };

  const handleSave = () => {
    if (!note.trim()) {
      Alert.alert("Error", "Please add a note");
      return;
    }
    Alert.alert("Success", "Report saved locally. Will sync when online.");
  };

  return (
    <ScrollView style={styles.container}>
      <Text style={styles.label}>Notes</Text>
      <TextInput
        style={styles.textArea}
        placeholder="Describe the work done..."
        value={note}
        onChangeText={setNote}
        multiline
        numberOfLines={4}
      />

      <Text style={styles.label}>Photos</Text>
      <TouchableOpacity style={styles.photoButton} onPress={handleTakePhoto}>
        <Text style={styles.photoButtonText}>Take Photo</Text>
      </TouchableOpacity>

      <Text style={styles.label}>QR Code</Text>
      <TouchableOpacity style={styles.qrButton} onPress={handleScanQR}>
        <Text style={styles.qrButtonText}>Scan Device QR</Text>
      </TouchableOpacity>

      <TouchableOpacity style={styles.saveButton} onPress={handleSave}>
        <Text style={styles.saveButtonText}>Save Report</Text>
      </TouchableOpacity>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
    backgroundColor: "#f5f5f5",
  },
  label: {
    fontSize: 16,
    fontWeight: "600",
    marginBottom: 8,
    color: "#333",
  },
  textArea: {
    backgroundColor: "#fff",
    padding: 12,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: "#ddd",
    height: 120,
    textAlignVertical: "top",
    marginBottom: 16,
  },
  photoButton: {
    backgroundColor: "#059669",
    padding: 12,
    borderRadius: 8,
    alignItems: "center",
    marginBottom: 16,
  },
  photoButtonText: {
    color: "#fff",
    fontSize: 14,
    fontWeight: "600",
  },
  qrButton: {
    backgroundColor: "#7c3aed",
    padding: 12,
    borderRadius: 8,
    alignItems: "center",
    marginBottom: 24,
  },
  qrButtonText: {
    color: "#fff",
    fontSize: 14,
    fontWeight: "600",
  },
  saveButton: {
    backgroundColor: "#2563eb",
    padding: 16,
    borderRadius: 8,
    alignItems: "center",
  },
  saveButtonText: {
    color: "#fff",
    fontSize: 16,
    fontWeight: "600",
  },
});
