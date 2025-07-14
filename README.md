# ThirdPlay Analytics SDK

ThirdPlay Analytics SDK is a Unity package that enables easy integration of Firebase Analytics into your Unity games. It provides a simple API to log events and parameters, helping you track user behavior and game metrics efficiently.

## Features
- Simple initialization and event logging
- Support for custom event parameters
- Seamless integration with Firebase Analytics

## Installation

### 1. Set Up Firebase in Your Project

1. Download the latest Firebase SDK for Unity from the [official Firebase website](https://firebase.google.com/download/unity).
2. Import the `FirebaseAnalytics.unitypackage` into your Unity project by dragging and dropping it into the Unity Editor.
3. Download your `google-services.json` file from the [Firebase Console](https://console.firebase.google.com/) and drag and drop it into your Unity project's `Assets` folder.

### 2. Add the ThirdPlay Analytics Package via Unity Package Manager

1. Open your Unity project.
2. Go to **Window > Package Manager**.
3. Click the **+** button and select **Add package from Git URL...**
4. Enter the GitHub repository URL for the package. For example:
   ```
   https://github.com/SteZeta75/Firebase4SAC.git
   ```
5. Click **Add**. Unity will download and import the package.

> **Note:** Make sure your project is set up for Firebase before importing this package.

## Usage

> **Important:**
> - Firebase Analytics event names must be alphanumeric (letters, numbers, or underscores) and start with a letter.
> - Event names must be less than 40 characters in length.
> - Avoid using spaces or special characters in event names.
> - You can log up to 500 unique event names per Firebase project.
> - For more details, refer to the [Firebase Analytics event naming guidelines](https://firebase.google.com/docs/analytics/events#event_name).

### 1. Initialize the Analytics System
Before logging any events, initialize the analytics system in your game's startup logic:

```csharp
using ThirdPlayAnalytics;

void Start() {
    AnalyticsManager.Initialize();
}
```

### 2. Log Events
#### Log a Simple Event
```csharp
// Log a simple event without parameters
AnalyticsManager.LogEvent("game_start");

// Log an event to know how many times the player has clicked the leaderboard button
AnalyticsManager.LogEvent("leaderboard_button_clicked");
```

#### Log an Event with Parameters
```csharp
AnalyticsManager.LogEvent("level_complete", "level_number", 2);
```

## API Reference
- `AnalyticsManager.Initialize()` – Initializes the analytics system.
- `AnalyticsManager.LogEvent(string eventName)` – Logs a simple event.
- `AnalyticsManager.LogEvent(string eventName, string paramName, int paramValue)` – Logs an event with a parameter.

## Support
For issues or feature requests, please open an issue on the [GitHub repository](https://github.com/your-org/ThirdPlayAnalytics).

---
© 2025 ThirdPlay. All rights reserved.
