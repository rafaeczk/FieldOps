import React from "react";
import { StatusBar } from "expo-status-bar";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { useAuthStore } from "./src/store/authStore";
import LoginScreen from "./src/screens/LoginScreen";
import WorkOrdersScreen from "./src/screens/WorkOrdersScreen";
import ReportScreen from "./src/screens/ReportScreen";
import type { RootStackParamList } from "./src/types/navigation";

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function App() {
  const { isAuthenticated } = useAuthStore();

  return (
    <NavigationContainer>
      <StatusBar style="auto" />
      <Stack.Navigator>
        {!isAuthenticated ? (
          <Stack.Screen
            name="Login"
            component={LoginScreen}
            options={{ headerShown: false }}
          />
        ) : (
          <>
            <Stack.Screen
              name="WorkOrders"
              component={WorkOrdersScreen}
              options={{ title: "Work Orders" }}
            />
            <Stack.Screen
              name="Report"
              component={ReportScreen}
              options={{ title: "Report" }}
            />
          </>
        )}
      </Stack.Navigator>
    </NavigationContainer>
  );
}
